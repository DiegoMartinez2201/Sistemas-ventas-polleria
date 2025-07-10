using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorRol : Controller
    {
        public IActionResult ListarRol()
        {
            List<entRol> lista = logRol.Instancia.ListarRol();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarRol()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertarRol(entRol c)
        {
            try
            {
                Boolean inserta = logRol.Instancia.InsertarRol(c);
                if (inserta)
                {
                    return RedirectToAction("ListarRol");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarRol", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaRol(int idRol)
        {
            entRol c = new entRol();
            c = logRol.Instancia.BuscarRol(idRol);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditaRol(entRol c)
        {
            try
            {
                Boolean edita = logRol.Instancia.EditaRol(c);
                if (edita)
                {
                    return RedirectToAction("ListarRol");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditaRol", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaRol(int idRol)
        {
            entRol rol = logRol.Instancia.BuscarRol(idRol);
            return View(rol);
        }
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarRol(int idRol)
        {
            try
            {
                entRol c = new entRol { idRol = idRol };
                bool resultado = logRol.Instancia.DeshabilitarRol(c);
                if (resultado)
                {
                    return RedirectToAction("ListarRol");
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
        public IActionResult ListarTodosRoles()
        {
            List<entRol> lista = logRol.Instancia.ListarTodosRoles();
            ViewBag.lista = lista;
            return View(lista);
        }
    }
}
