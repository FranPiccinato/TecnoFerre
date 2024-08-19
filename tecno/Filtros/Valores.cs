using Microsoft.AspNetCore.Mvc.Filters;
using tecno.Servicio;
using tecno.Models;
using Microsoft.AspNetCore.Mvc;

namespace tecno.Filtros
{
    public class Valores : IAsyncActionFilter
    {
        private readonly IServicio_API _servicioApi;
        private readonly AppDbContext _appDbContext;

        public Valores(IServicio_API servicioApi, AppDbContext appDbContext)
        {
            _servicioApi = servicioApi;
            _appDbContext = appDbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller as Controller;

            if (controller != null)
            {
                var lista = await _servicioApi.Lista();

                var categorias = lista.Select(p => p.categoria).Distinct().ToList();

                controller.ViewBag.Categorias = categorias;
                controller.ViewBag.buscar = lista;
            }
            await next();
        }
    }
}
