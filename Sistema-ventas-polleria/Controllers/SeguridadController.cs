using Microsoft.AspNetCore.Mvc;
using capaLogica;
using capaEntidad;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Sistema_ventas_polleria.Controllers
{
    public class SeguridadController : Controller
    {
        private logSesionUsuario objLogSesion = new logSesionUsuario();
        private logLogActividad objLogActividad = new logLogActividad();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string contraseña)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                TempData["Error"] = "Correo y contraseña son obligatorios";
                return View();
            }

            // Buscar usuario en la base de datos
            var usuario = logUsuario.Instancia.ListarUsuario()
                .FirstOrDefault(u => u.correo == correo && u.contraseña == contraseña && u.estado == 1);

            if (usuario == null)
            {
                TempData["Error"] = "Usuario o contraseña incorrectos";
                return View();
            }

            // Crear sesión solo si el usuario existe
            var sesion = new entSesionUsuario
            {
                idUsuario = usuario.idUsuario,
                token = objLogSesion.GenerarToken(),
                fechaExpiracion = objLogSesion.CalcularExpiracion(),
                ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                userAgent = HttpContext.Request.Headers["User-Agent"].ToString(),
                activa = true
            };

            string resultado = objLogSesion.CrearSesion(sesion);
            if (resultado == "OK")
            {
                HttpContext.Session.SetString("Token", sesion.token);
                HttpContext.Session.SetInt32("IdUsuario", sesion.idUsuario);

                // Registrar actividad
                objLogActividad.RegistrarActividadSimple(sesion.idUsuario, "LOGIN", "Usuario", sesion.idUsuario, sesion.ipAddress);

                // Redirección según rol
                if (usuario.idRol == 1)
                    return RedirectToAction("Dashboard", "Administrador");
                else if (usuario.idRol == 2)
                    return RedirectToAction("Index", "Vendedor"); // Cambia según tu controlador de vendedor
                else // Cliente
                    return RedirectToAction("Clientelog", "Cliente");
            }
            else
            {
                TempData["Error"] = "Error al crear sesión";
                return View();
            }
        }

        public IActionResult Logout()
        {
            string token = HttpContext.Session.GetString("Token");
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (!string.IsNullOrEmpty(token))
            {
                objLogSesion.CerrarSesion(token);

                // Registrar actividad
                if (idUsuario.HasValue)
                {
                    objLogActividad.RegistrarActividadSimple(idUsuario.Value, "LOGOUT", "Usuario", idUsuario.Value);
                }
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult ValidarSesion()
        {
            string token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return Json(new { valido = false });
            }

            DataTable dt = objLogSesion.ValidarSesion(token);
            bool valido = dt.Rows.Count > 0;

            if (!valido)
            {
                HttpContext.Session.Clear();
            }

            return Json(new { valido = valido });
        }

        public IActionResult VerificarPermiso(string permiso)
        {
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue)
            {
                return Json(new { tienePermiso = false });
            }

            bool tienePermiso = objLogSesion.VerificarPermiso(idUsuario.Value, permiso);
            return Json(new { tienePermiso = tienePermiso });
        }

        public IActionResult Logs()
        {
            // Solo administradores pueden ver logs
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "ADMIN_SISTEMA"))
            {
                return RedirectToAction("Index", "Home");
            }

            DateTime fechaInicio = DateTime.Now.AddDays(-7);
            DateTime fechaFin = DateTime.Now;

            DataTable dt = objLogActividad.ListarLogs(fechaInicio, fechaFin);
            return View(dt);
        }

        [HttpPost]
        public IActionResult LimpiarLogs(int diasRetener = 90)
        {
            // Solo administradores pueden limpiar logs
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "ADMIN_SISTEMA"))
            {
                return Json(new { success = false, message = "Sin permisos" });
            }

            string resultado = objLogActividad.LimpiarLogsAntiguos(diasRetener);
            return Json(new { success = resultado == "OK", message = resultado });
        }
    }
} 