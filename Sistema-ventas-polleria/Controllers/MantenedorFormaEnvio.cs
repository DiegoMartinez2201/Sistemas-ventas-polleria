using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorFormaEnvio : Controller
    {
        public IActionResult ListarFormaEnvio()
        {
            List<entFormaEnvio> lista = logFormaEnvio.Instancia.ListarFormaEnvio();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarFormaEnvio()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarFormaEnvio(entFormaEnvio c)
        {
            try
            {
                Boolean inserta = logFormaEnvio.Instancia.InsertarFormaEnvio(c);
                if (inserta)
                {
                    return RedirectToAction("ListarFormaEnvio");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarFormaEnvio", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaFormaEnvio(int idFormaEnvio)
        {
            entFormaEnvio c = new entFormaEnvio();
            c = logFormaEnvio.Instancia.BuscarFormaEnvio(idFormaEnvio);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditaFormaEnvio(entFormaEnvio c)
        {
            try
            {
                Boolean edita = logFormaEnvio.Instancia.EditaFormaEnvio(c);
                if (edita)
                {
                    return RedirectToAction("ListarFormaEnvio");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditaFormaEnvio", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaFormaEnvio(int idFormaEnvio)
        {
            entFormaEnvio formaEnvio = logFormaEnvio.Instancia.BuscarFormaEnvio(idFormaEnvio);
            return View(formaEnvio);
        }
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarFormaEnvio(int idFormaEnvio)
        {
            try
            {
                entFormaEnvio c = new entFormaEnvio { idFormaEnvio = idFormaEnvio };
                bool resultado = logFormaEnvio.Instancia.DeshabilitarFormaEnvio(c);
                if (resultado)
                {
                    return RedirectToAction("ListarFormaEnvio");
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
