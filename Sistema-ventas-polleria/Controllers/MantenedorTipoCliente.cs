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
        [HttpGet]
        public ActionResult InsertarTipoCliente()
        {
          return View();
        }
        [HttpPost]
        public ActionResult InsertarTipoCliente(entTipoCliente c)
        {
            try
            {
                Boolean inserta = logTipoCliente.Instancia.InsertarTipoCliente(c);
                if (inserta)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex) {
                return RedirectToAction("InsertarTipoCliente", new {msjExceptio = ex.Message});
            }
        }
        [HttpGet]
        public ActionResult EditaTipoCliente(int idTipoCliente)
        {
            entTipoCliente c = new entTipoCliente();
            c = logTipoCliente.Instancia.BuscarTipoCliente(idTipoCliente);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditaTipoCliente(entTipoCliente c)
        {
            try
            {
                Boolean edita = logTipoCliente.Instancia.EditaTipoCliente(c);
                if (edita)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex) {
                return RedirectToAction("EditaTipoCliente", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaTipoCliente()
        {
            List<entTipoCliente> listaTipoCliente = logTipoCliente.Instancia.ListarTipoCliente();
            return View(listaTipoCliente);
        }
        [HttpPost]
        public ActionResult DeshabilitaTipoCliente(entTipoCliente c)
        {
            try
            {
                bool resultado = logTipoCliente.Instancia.DeshabilitarTipoCliente(c);
                if (resultado)
                {
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View("Error");
                }
            }
            catch(Exception ex)
            {
                return View("Error", new {message = ex.Message});
            }
        }
    }
}
