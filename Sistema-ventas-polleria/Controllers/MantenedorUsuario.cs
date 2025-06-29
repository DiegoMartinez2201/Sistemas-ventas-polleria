using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;
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
            // Cargar los roles desde la base de datos
            List<entRol> listaRoles = logRol.Instancia.ListarRol();

            // Crear SelectList para el dropdown de roles
            ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

            return View();
        }
        [HttpPost]
        public ActionResult InsertarUsuario(entUsuario c)
        {
            try
            {
                // Recargar los roles en caso de error de validación
                List<entRol> listaRoles = logRol.Instancia.ListarRol();
                ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

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
                // Recargar los roles en caso de excepción
                List<entRol> listaRoles = logRol.Instancia.ListarRol();
                ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

                return RedirectToAction("InsertarUsuario", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaUsuario(int idUsuario)
        {
            entUsuario c = new entUsuario();
            c = logUsuario.Instancia.BuscarUsuario(idUsuario);

            // Cargar los roles para el dropdown
            List<entRol> listaRoles = logRol.Instancia.ListarRol();
            ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

            return View(c);
        }
        [HttpPost]
        public ActionResult EditaUsuario(entUsuario c)
        {
            try
            {
                // Recargar los roles en caso de error de validación
                List<entRol> listaRoles = logRol.Instancia.ListarRol();
                ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

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
                // Recargar los roles en caso de excepción
                List<entRol> listaRoles = logRol.Instancia.ListarRol();
                ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

                return RedirectToAction("EditaUsuario", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaUsuario(int idUsuario)
        {
            entUsuario usuario = logUsuario.Instancia.BuscarUsuario(idUsuario);
            return View(usuario);
        }
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarUsuario(int idUsuario)
        {
            try
            {
                entUsuario c = new entUsuario { idUsuario = idUsuario };
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
