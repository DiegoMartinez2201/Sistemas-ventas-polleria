using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorCategoria : Controller
    {
        public IActionResult ListarCategoria()
        {
            List<entCategoria> lista = logCategoria.Instancia.ListarCategoria();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarCategoria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarCategoria(entCategoria c)
        {
            try
            {
                Boolean inserta = logCategoria.Instancia.InsertarCategoria(c);
                if (inserta)
                {
                    return RedirectToAction("ListarCategoria");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarCategoria", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditarCategoria(int idCategoria)
        {
            entCategoria c = new entCategoria();
            c = logCategoria.Instancia.BuscarCategoria(idCategoria);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditarCategoria(entCategoria c)
        {
            try
            {
                Boolean edita = logCategoria.Instancia.EditarCategoria(c);
                if (edita)
                {
                    return RedirectToAction("ListarCategoria");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditaCategoria", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitarCategoria()
        {
            List<entCategoria> listaCategoria = logCategoria.Instancia.ListarCategoria();
            return View(listaCategoria);
        }
        [HttpPost]
        public ActionResult DeshabilitarCategoria(entCategoria c)
        {
            try
            {
                bool resultado = logCategoria.Instancia.DeshabilitarCategoria(c);
                if (resultado)
                {
                    return RedirectToAction("ListarCategoria");
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
