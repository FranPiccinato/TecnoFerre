using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using tecno.Models;
using tecno.Servicio;

namespace tecno.Controllers
{
    public class CarritoController: Controller
    {
        private readonly AppDbContext _context;
        private readonly IServicio_API _servicioApi;

       
        

        public CarritoController(IServicio_API servicioApi, AppDbContext context)
        {
            _context = context;
            _servicioApi = servicioApi;
        }

        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Carrito()
        {
            // Obtener la cédula del usuario autenticado
            string cedula = User.Identity.GetCedula();

            int cedulaInt;
            if (int.TryParse(cedula, out cedulaInt))
            {
                var carrito = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Producto)
                    .FirstOrDefaultAsync(c => c.UserId == cedulaInt);

                if (carrito == null || carrito.CartItems == null)
                {
                    return View(new List<CartItem>()); 
                }

                return View(carrito.CartItems);
            }
            else
            {
                return BadRequest("ID de usuario no válido");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAlCarrito(int productId, int quantity)
        {
            string cedula = User.Identity.GetCedula();
            int cedulaInt;
            if (int.TryParse(cedula, out cedulaInt))
            {
                var carrito = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == cedulaInt);

                if (carrito == null)
                {
                    carrito = new Cart { UserId = cedulaInt, CreatedDate = DateTime.Now, CartItems = new List<CartItem>() };
                    _context.Carts.Add(carrito);
                    await _context.SaveChangesAsync();
                }

                var producto = await _context.Productos.FindAsync(productId);

                if (producto == null)
                {
                    return NotFound();
                }

                if (carrito.CartItems == null)
                {
                    carrito.CartItems = new List<CartItem>();
                }

                var cartItem = carrito.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem == null)
                {
                    cartItem = new CartItem
                    {
                        CartId = carrito.CartId,
                        ProductId = productId,
                        Quantity = quantity,
                        Price = producto.precio
                    };
                    _context.CartItems.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Carrito");
            }
            else
            {
                
                return BadRequest("ID de usuario no válido");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> EliminarDelCarrito(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Carrito");
        }
    }

}

