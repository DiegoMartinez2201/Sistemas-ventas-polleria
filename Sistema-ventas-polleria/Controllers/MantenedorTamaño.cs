using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorTamaño : Controller
    {
        public IActionResult ListarTamaño()
        {
            List<entTamaño> lista = logTamaño.Instancia.ListarTamaño();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarTamaño()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarTamaño(entTamaño c)
        {
            try
            {
                Boolean inserta = logTamaño.Instancia.InsertarTamaño(c);
                if (inserta)
                {
                    return RedirectToAction("ListarTamaño");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarTamaño", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditarTamaño(int idTamaño)
        {
            entTamaño c = new entTamaño();
            c = logTamaño.Instancia.BuscarTamaño(idTamaño);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditarTamaño(entTamaño c)
        {
            try
            {
                Boolean edita = logTamaño.Instancia.EditarTamaño(c);
                if (edita)
                {
                    return RedirectToAction("ListarTamaño");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditarTamaño", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitarTamaño()
        {
            List<entTamaño> listaTamaño = logTamaño.Instancia.ListarTamaño();
            return View(listaTamaño);
        }
        [HttpPost]
        public ActionResult DeshabilitarTamaño(entTamaño c)
        {
            try
            {
                bool resultado = logTamaño.Instancia.DeshabilitarTamaño(c);
                if (resultado)
                {
                    return RedirectToAction("ListarTamaño");
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
