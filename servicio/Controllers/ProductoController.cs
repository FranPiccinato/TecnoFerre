using Microsoft.AspNetCore.Mvc;
using servicio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;


namespace ServicioApi.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;


        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarProductos()
        {
            try
            {
                var listaProductos = await _context.Productos.ToListAsync();
                return Ok(new { mensaje = "ok", response = listaProductos });
            }
            catch (Exception error)
            {
                return StatusCode(500, new { mensaje = error.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "agregado" });
            }
            catch (Exception error)
            {
                return StatusCode(500, new { mensaje = error.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado" });
                }
                return Ok(new { mensaje = "ok", response = producto });
            }
            catch (Exception error)
            {
                return StatusCode(500, new { mensaje = error.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Producto producto)
        {
            try
            {
                var productoExistente = await _context.Productos.FindAsync(producto.id);
                if (productoExistente == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado" });
                }

                productoExistente.nombre = producto.nombre;
                productoExistente.categoria = producto.categoria;
                productoExistente.precio = producto.precio;
                productoExistente.descripcion = producto.descripcion;
                productoExistente.imagen = producto.imagen;
                productoExistente.fecha_ingreso = DateTime.Now;

                _context.Productos.Update(productoExistente);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "editado" });
            }
            catch (Exception error)
            {
                return StatusCode(500, new { mensaje = error.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado" });
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {
                return StatusCode(500, new { mensaje = error.Message });
            }
        }


    }
}

