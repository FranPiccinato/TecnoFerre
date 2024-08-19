using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tecno.Models;
using System.Linq;
using System.Threading.Tasks;

namespace tecno.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> MisPedidos()
        {
            // Obtener el ID del usuario autenticado
            int userId = int.Parse(User.Identity.GetCedula());

         
            var facturas = await _context.Facturas
                .Include(f => f.FacturaItems)
                .ThenInclude(fi => fi.Producto)
                .Include(f => f.Mensajero)
                .Where(f => f.id_usuario == userId)
                .ToListAsync();

            // Pasar las facturas a la vista
            return View("MisPedidos", facturas);
        }
    }
}
