using Microsoft.AspNetCore.Mvc;
using capaEntidad;
using capaLogica;
namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorTipoCliente : Controller
    {
        public IActionResult Listar()
        {
            List<entTipoCliente> lista = logTipoCliente.Instancia.ListarTipoCliente();
            ViewBag.lista = lista;
            return View(lista);
        }

    }
}
