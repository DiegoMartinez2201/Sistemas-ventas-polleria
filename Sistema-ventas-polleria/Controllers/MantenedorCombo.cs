using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorCombo : Controller
    {
        public IActionResult ListarCombo()
        {
            List<entCombo> lista = logCombo.Instancia.ListarCombo();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarCombo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarCombo(entCombo c)
        {
            try
            {
                Boolean inserta = logCombo.Instancia.InsertarCombo(c);
                if (inserta)
                {
                    return RedirectToAction("ListarCombo");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarCombo", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaCombo(int idCombo)
        {
            entCombo c = new entCombo();
            c = logCombo.Instancia.BuscarCombo(idCombo);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditaCombo(entCombo c)
        {
            try
            {
                Boolean edita = logCombo.Instancia.EditaCombo(c);
                if (edita)
                {
                    return RedirectToAction("ListarCombo");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditaCombo", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaCombo(int idCombo)
        {
            entCombo combo = logCombo.Instancia.BuscarCombo(idCombo);
            return View(combo);
        }
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarRol(int idCombo)
        {
            try
            {
                entCombo c = new entCombo { idCombo = idCombo };
                bool resultado = logCombo.Instancia.DeshabilitarCombo(c);
                if (resultado)
                {
                    return RedirectToAction("ListarCombo");
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
