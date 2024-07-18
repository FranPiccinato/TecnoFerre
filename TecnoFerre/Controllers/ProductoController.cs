using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using TecnoFerre.Models;
using System.IO;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using TecnoFerre.Servicio;
using System.Diagnostics;

namespace TecnoFerre.Controllers
{
    public class ProductoController : Controller
    {
        private IServicio_API _servicioApi;

        public ProductoController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Admin()
        {
            List<Producto> lista = await _servicioApi.Lista();
            return View(lista);
        }

        public async Task<IActionResult> Producto(int idProducto)
        {

            Producto modelo_producto = new Producto();

            ViewBag.Accion = "Nuevo Producto";

            if (idProducto != 0)
            {

                ViewBag.Accion = "Editar Producto";
                modelo_producto = await _servicioApi.Obtener(idProducto);
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
        public async Task<IActionResult> Eliminar(int idProducto)
        {

            var respuesta = await _servicioApi.Eliminar(idProducto);

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
