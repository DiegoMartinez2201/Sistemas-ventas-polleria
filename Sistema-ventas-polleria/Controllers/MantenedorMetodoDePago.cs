using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorMetodoDePago : Controller
    {
        public IActionResult ListarMetodoDePago()
        {
            List<entMetodoDePago> lista = logMetodoDePago.Instancia.ListarMetodoDePago();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarMetodoDePago()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarMetodoDePago(entMetodoDePago c)
        {
            try
            {
                Boolean inserta = logMetodoDePago.Instancia.InsertarMetodoDePago(c);
                if (inserta)
                {
                    return RedirectToAction("ListarMetodoDePago");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarMetodoDePago", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditarMetodoDePago(int idMetodoPago)
        {
            entMetodoDePago c = new entMetodoDePago();
            c = logMetodoDePago.Instancia.BuscarMetodoDePago(idMetodoPago);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditarMetodoDePago(entMetodoDePago c)
        {
            try
            {
                Boolean edita = logMetodoDePago.Instancia.InsertarMetodoDePago(c);
                if (edita)
                {
                    return RedirectToAction("ListarMetodoDePago");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditarMetodoDePago", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitarMetodoDePago(int idMetodoPago)
        {
            entMetodoDePago metodoPago = logMetodoDePago.Instancia.BuscarMetodoDePago(idMetodoPago);
            return View(metodoPago);
        }
        [HttpPost]
        public ActionResult DeshabilitarMetodoDePago(entMetodoDePago c)
        {
            try
            {
                bool resultado = logMetodoDePago.Instancia.DeshabilitarMetodoDePago(c);
                if (resultado)
                {
                    return RedirectToAction("ListarMetodoDePago");
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
        public ActionResult ConfirmarDeshabilitarMetodoDePago(entMetodoDePago c)
        {
            try
            {
                bool resultado = logMetodoDePago.Instancia.DeshabilitarMetodoDePago(c);
                if (resultado)
                {
                    return RedirectToAction("ListarMetodoDePago");
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
        public IActionResult ListarTodosMetodosDePago()
        {
            List<entMetodoDePago> lista = logMetodoDePago.Instancia.ListarTodosMetodosDePago();
            ViewBag.lista = lista;
            return View(lista);
        }
    }
}
