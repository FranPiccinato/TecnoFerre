using Microsoft.AspNetCore.Mvc;
using tecno.Models;
using tecno.Servicio;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;


namespace tecno.Controllers
{
    public class ProductoController : Controller
    {
        private IServicio_API _servicioApi;

        public ProductoController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Admin()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Index()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Producto(int id)
        {
            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if (id != 0)
            {

                ViewBag.Accion = "Editar Producto";
                modelo_producto = await _servicioApi.Obtener(id);
            }

            return View(modelo_producto);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios(Producto ob_producto)
        {

            bool respuesta;

            if (ob_producto.id == 0)
            {
                respuesta = await _servicioApi.Guardar(ob_producto);
            }
            else
            {
                respuesta = await _servicioApi.Editar(ob_producto);
            }


            if (respuesta)
                return RedirectToAction("Admin");
            else
                return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var respuesta = await _servicioApi.Eliminar(id);

            if (respuesta)
                return RedirectToAction("Admin");
            else
                return NoContent();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
