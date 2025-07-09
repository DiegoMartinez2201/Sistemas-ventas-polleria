using capa;
using capaLogica;
using capaEntidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorUsuario : Controller
    {
        private logLogActividad objLogActividad = new logLogActividad();
        private logSesionUsuario objLogSesion = new logSesionUsuario();

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
                // Verificar permisos
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
                if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "USUARIO_CREAR"))
                {
                    TempData["Error"] = "No tiene permisos para crear usuarios";
                    return RedirectToAction("ListarUsuario");
                }

                // Recargar los roles en caso de error de validación
                List<entRol> listaRoles = logRol.Instancia.ListarRol();
                ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

                Boolean inserta = logUsuario.Instancia.InsertarUsuario(c);
                if (inserta)
                {
                    // Registrar actividad
                    objLogActividad.RegistrarActividadSimple(idUsuario.Value, "INSERTAR", "Usuario", c.idUsuario, HttpContext.Connection.RemoteIpAddress?.ToString());
                    
                    TempData["Mensaje"] = "Usuario insertado correctamente";
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
                // Verificar permisos
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
                if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "USUARIO_EDITAR"))
                {
                    TempData["Error"] = "No tiene permisos para editar usuarios";
                    return RedirectToAction("ListarUsuario");
                }

                // Recargar los roles en caso de error de validación
                List<entRol> listaRoles = logRol.Instancia.ListarRol();
                ViewBag.Roles = new SelectList(listaRoles, "idRol", "nombreRol");

                Boolean edita = logUsuario.Instancia.EditaUsuario(c);
                if (edita)
                {
                    // Registrar actividad
                    objLogActividad.RegistrarActividadSimple(idUsuario.Value, "EDITAR", "Usuario", c.idUsuario, HttpContext.Connection.RemoteIpAddress?.ToString());
                    
                    TempData["Mensaje"] = "Usuario editado correctamente";
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
                // Verificar permisos
                int? idUsuarioSesion = HttpContext.Session.GetInt32("IdUsuario");
                if (!idUsuarioSesion.HasValue || !objLogSesion.VerificarPermiso(idUsuarioSesion.Value, "USUARIO_ELIMINAR"))
                {
                    TempData["Error"] = "No tiene permisos para deshabilitar usuarios";
                    return RedirectToAction("ListarUsuario");
                }

                entUsuario c = new entUsuario { idUsuario = idUsuario };
                bool resultado = logUsuario.Instancia.DeshabilitarUsuario(c);
                if (resultado)
                {
                    // Registrar actividad
                    objLogActividad.RegistrarActividadSimple(idUsuarioSesion.Value, "DESHABILITAR", "Usuario", idUsuario, HttpContext.Connection.RemoteIpAddress?.ToString());
                    
                    TempData["Mensaje"] = "Usuario deshabilitado correctamente";
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
