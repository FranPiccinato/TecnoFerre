using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tecno.Models;
using tecno.Servicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace tecno.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicio_API _servicioApi;
        private readonly AppDbContext _appDbContext;

        public HomeController(IServicio_API servicioApi, AppDbContext appDbContext)
        {
            _servicioApi = servicioApi;
            _appDbContext = appDbContext;
        }
      

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Obtener la lista de productos
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Ayuda()
        {
            return View();
        }

        public async Task<IActionResult> informacionProducto()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> resultadoProducto()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Categorias()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }


        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Usuario()
        {
            string cedula = User.Identity.GetCedula(); // Asegúrate de que este método existe
            return View("Usuario", cedula);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
