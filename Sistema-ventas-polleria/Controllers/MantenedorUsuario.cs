using Microsoft.AspNetCore.Mvc;
using capaEntidad;
using capaLogica;
using capa;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorUsuario : Controller
    {
        public IActionResult ListarUsuario()
        {
            List<entUsuario> lista = logUsuario.Instancia.ListarUsuario();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarUsuario()
        {
            var roles = logRol.Instancia.ListarRol();
            ViewBag.Roles = new SelectList(roles, "idRol", "nombreRol");
            return View();
        }
        [HttpPost]
        public ActionResult InsertarUsuario(entUsuario c)
        {
            try
            {
                Boolean inserta = logUsuario.Instancia.InsertarUsuario(c);
                if (inserta)
                {
                    return RedirectToAction("ListarUsuario");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("InsertarUsuario", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaUsuario(int idUsuario)
        {
            entUsuario c = new entUsuario();
            c = logUsuario.Instancia.BuscarUsuario(idUsuario);
            return View(c);
        }
        [HttpPost]
        public ActionResult EditaUsuario(entUsuario c)
        {
            try
            {
                Boolean edita = logUsuario.Instancia.EditaUsuario(c);
                if (edita)
                {
                    return RedirectToAction("ListarUsuario");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("EditaUsuario", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaUsuario()
        {
            List<entUsuario> listaUsuario = logUsuario.Instancia.ListarUsuario();
            return View(listaUsuario);
        }
        [HttpPost]
        public ActionResult DeshabilitaUsuario(entUsuario c)
        {
            try
            {
                bool resultado = logUsuario.Instancia.DeshabilitarUsuario(c);
                if (resultado)
                {
                    return RedirectToAction("ListarUsuario");
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
