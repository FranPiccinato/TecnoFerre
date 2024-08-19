using Microsoft.AspNetCore.Mvc;
using tecno.Servicio;
using tecno.Models;
using Microsoft.EntityFrameworkCore;

namespace tecno.Controllers
{
    public class FacturaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IServicio_API _servicioApi;

        public FacturaController(IServicio_API servicioApi, AppDbContext context)
        {
            _context = context;
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Factura([Bind(Prefix = "id")] int id)
        {
            // Obteniendo el carrito asociado
            var carrito = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Producto)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.CartId == id);

            if (carrito == null)
            {
                return NotFound();
            }


            if (!carrito.CartItems.Any())
            {
                return RedirectToAction("Carrito", "Carrito");
            }

            // Calculando el subtotal y el total
            var subtotal = carrito.CartItems.Sum(ci => ci.Price * ci.Quantity);

            var impuesto = 0.13m;
            ViewBag.Impuesto = Math.Round(subtotal * impuesto, 2);
            var impuestoTotal = Math.Round(subtotal * (1 + impuesto), 2);
            var total = impuestoTotal;

            // Obtiene una lista de mensajeros disponibles
            var mensajeros = await _context.Usuarios
                .Where(u => u.rol == 3) // Suponiendo que el rol 3 es para mensajeros
                .ToListAsync();

            // Selección aleatoria de mensajero (opcional)
            var random = new Random();
            var mensajero = mensajeros[random.Next(mensajeros.Count)];

            // Creación de la factura
            var factura = new Factura
            {
                nFactura = generarNumero(),
                id_usuario = carrito.UserId,
                usuario = carrito.Usuario,
                fechaEmision = DateTime.Now,
                subtotal = subtotal,
                total = total,
                id_carrito = carrito.CartId,
                carrito = carrito,
                Estado = "Pendiente",  // Estado inicial como pendiente
                FacturaItems = carrito.CartItems.Select(ci => new FacturaItem
                {
                    ProductoId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Price
                }).ToList(),
                id_mensajero = null // No se asigna mensajero de inmediato
            };

            // Guardar la factura en la base de datos
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            // Limpiar el carrito
            _context.CartItems.RemoveRange(carrito.CartItems);
            await _context.SaveChangesAsync();

            // Devolver la vista de la factura generada
            return View("Factura", factura);
        }

        private string generarNumero()
        {
            return $"FAC-{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}
