using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecnoFerre.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TecnoFerre.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly AppDbContext _context;

        public TransaccionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Transaccion/Crear
        public IActionResult Create()
        {
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "Nombre");
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre");
            return View();
        }

        // POST: Transaccion/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransaccionID,UsuarioID,ProductoID,FechaTransaccion,Cantidad,Estado")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoID"] = new SelectList(_context.Productos, "ProductoID", "Nombre", transaccion.ProductoID);
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nombre", transaccion.UsuarioID);
            return View(transaccion);
        }

        // GET: Transaccion/Anular/5
        public async Task<IActionResult> Anular(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccion = await _context.Transacciones
                .FirstOrDefaultAsync(m => m.TransaccionID == id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return View(transaccion);
        }

        // POST: Transaccion/Anular/5
        [HttpPost, ActionName("Anular")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnularConfirmed(int id)
        {
            var transaccion = await _context.Transacciones.FindAsync(id);
            if (transaccion != null)
            {
                transaccion.Estado = "Anulada";
                _context.Update(transaccion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Transaccion
        public async Task<IActionResult> Index()
        {
            var transacciones = _context.Transacciones.Include(t => t.Producto).Include(t => t.Usuario);
            return View(await transacciones.ToListAsync());
        }
    }
}
