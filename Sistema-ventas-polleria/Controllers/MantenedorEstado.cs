using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorEstado : Controller
    {
        public IActionResult ListarEstado()
        {
            List<entEstado> lista = logEstado.Instancia.ListarEstado();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarEstado()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarEstado(entEstado c)
        {
            try
            {
                Boolean inserta = logEstado.Instancia.InsertarEstado(c);
                if (inserta)
                {
                    return RedirectToAction("ListarEstado");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarEstado", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaEstado(int idEstado)
        {
            entEstado c = new entEstado();
            c = logEstado.Instancia.BuscarEstado(idEstado);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditaEstado(entEstado c)
        {
            try
            {
                Boolean edita = logEstado.Instancia.EditaEstado(c);
                if (edita)
                {
                    return RedirectToAction("ListarEstado");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditaEstado", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaEstado(int idEstado)
        {
            entEstado estado = logEstado.Instancia.BuscarEstado(idEstado);
            return View(estado);
        }
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarEstado(int idEstado)
        {
            try
            {
                entEstado c = new entEstado { idEstado = idEstado };
                bool resultado = logEstado.Instancia.DeshabilitarEstado(c);
                if (resultado)
                {
                    return RedirectToAction("ListarEstado");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error", new { message = ex.Message });
            }
        }
    }
}
