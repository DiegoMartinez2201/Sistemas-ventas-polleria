using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorMarca : Controller
    {
        public IActionResult ListarMarca()
        {
            List<entMarca> lista = logMarca.Instancia.ListarMarca();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarMarca()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarMarca(entMarca c)
        {
            try
            {
                Boolean inserta = logMarca.Instancia.InsertarMarca(c);
                if (inserta)
                {
                    return RedirectToAction("ListarMarca");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarMarca", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditarMarca(int idMarca)
        {
            entMarca c = new entMarca();
            c = logMarca.Instancia.BuscarMarca(idMarca);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditarMarca(entMarca c)
        {
            try
            {
                Boolean edita = logMarca.Instancia.EditarMarca(c);
                if (edita)
                {
                    return RedirectToAction("ListarMarca");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditarMarca", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitarMarca(int idMarca)
        {
            entMarca marca = logMarca.Instancia.BuscarMarca(idMarca);
            return View(marca);
        }
        [HttpPost]
        public ActionResult DeshabilitarMarca(entMarca c)
        {
            try
            {
                bool resultado = logMarca.Instancia.DeshabilitarMarca(c);
                if (resultado)
                {
                    return RedirectToAction("ListarMarca");
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
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarMarca(entMarca c)
        {
            try
            {
                bool resultado = logMarca.Instancia.DeshabilitarMarca(c);
                if (resultado)
                {
                    return RedirectToAction("ListarMarca");
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
        public IActionResult ListarTodasMarcas()
        {
            List<entMarca> lista = logMarca.Instancia.ListarTodasMarcas();
            ViewBag.lista = lista;
            return View(lista);
        }
    }
    
}
