using Microsoft.AspNetCore.Mvc;
using tecno.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace tecno.Controllers
{

    public class AdminController : Controller
    {
        private readonly AppDbContext _context;


        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GestionarPedidos()
        {
            var pedidos = await _context.Facturas
                .Include(f => f.FacturaItems)
                .ThenInclude(fi => fi.Producto)
                .Include(f => f.Mensajero)
                .Include(f => f.usuario)
                .ToListAsync();

            ViewBag.Mensajeros = await _context.Usuarios
             .Where(u => u.rol == 3)
             .ToListAsync();

            return View("~/Views/Administrador/GestionarPedidos.cshtml", pedidos);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> AsignarMensajero(int pedidoId, int mensajeroId)
        {
            var pedido = await _context.Facturas.FindAsync(pedidoId);

            if (pedido == null)
            {
                ModelState.AddModelError("", "Pedido no encontrado.");
                return RedirectToAction("GestionarPedidos");
            }

            pedido.id_mensajero = mensajeroId;

            await _context.SaveChangesAsync();



            return await GestionarPedidos();
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int pedidoId, string estado)
        {
            var pedido = await _context.Facturas.FindAsync(pedidoId);

            if (pedido == null)
            {
                return NotFound();
            }

            pedido.Estado = estado;

            await _context.SaveChangesAsync();



            return await GestionarPedidos();
        }
    }
}