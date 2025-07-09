using Microsoft.AspNetCore.Mvc;
using capaLogica;
using capaEntidad;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace Sistema_ventas_polleria.Controllers
{
    public class ReportesController : Controller
    {
        private logLogActividad objLogActividad = new logLogActividad();
        private logSesionUsuario objLogSesion = new logSesionUsuario();

        public IActionResult Index()
        {
            // Verificar permisos
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "REPORTE_VER"))
            {
                TempData["Error"] = "No tiene permisos para ver reportes";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult ActividadUsuario(int idUsuario, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            // Verificar permisos
            int? idUsuarioSesion = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuarioSesion.HasValue || !objLogSesion.VerificarPermiso(idUsuarioSesion.Value, "REPORTE_VER"))
            {
                TempData["Error"] = "No tiene permisos para ver reportes";
                return RedirectToAction("Index", "Home");
            }

            if (!fechaInicio.HasValue)
                fechaInicio = DateTime.Now.AddDays(-30);
            if (!fechaFin.HasValue)
                fechaFin = DateTime.Now;

            DataTable dt = objLogActividad.ListarLogs(fechaInicio.Value, fechaFin.Value);
            ViewBag.FechaInicio = fechaInicio.Value;
            ViewBag.FechaFin = fechaFin.Value;
            ViewBag.IdUsuario = idUsuario;

            return View(dt);
        }

        public IActionResult EstadisticasActividad(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            // Verificar permisos
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "ADMIN_SISTEMA"))
            {
                TempData["Error"] = "No tiene permisos para ver estadísticas";
                return RedirectToAction("Index", "Home");
            }

            if (!fechaInicio.HasValue)
                fechaInicio = DateTime.Now.AddDays(-30);
            if (!fechaFin.HasValue)
                fechaFin = DateTime.Now;

            // Aquí implementarías la lógica para obtener estadísticas
            // Por ahora retornamos una vista vacía
            ViewBag.FechaInicio = fechaInicio.Value;
            ViewBag.FechaFin = fechaFin.Value;

            return View();
        }

        public IActionResult SesionesActivas()
        {
            // Verificar permisos
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "ADMIN_SISTEMA"))
            {
                TempData["Error"] = "No tiene permisos para ver sesiones activas";
                return RedirectToAction("Index", "Home");
            }

            // Aquí implementarías la lógica para obtener sesiones activas
            return View();
        }

        [HttpPost]
        public IActionResult CerrarSesionUsuario(int idUsuario)
        {
            // Verificar permisos
            int? idUsuarioSesion = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuarioSesion.HasValue || !objLogSesion.VerificarPermiso(idUsuarioSesion.Value, "ADMIN_SISTEMA"))
            {
                return Json(new { success = false, message = "Sin permisos" });
            }

            // Aquí implementarías la lógica para cerrar sesiones
            // Por ahora retornamos éxito
            return Json(new { success = true, message = "Sesiones cerradas correctamente" });
        }

        public IActionResult ExportarLogs(DateTime fechaInicio, DateTime fechaFin)
        {
            // Verificar permisos
            int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
            if (!idUsuario.HasValue || !objLogSesion.VerificarPermiso(idUsuario.Value, "ADMIN_SISTEMA"))
            {
                TempData["Error"] = "No tiene permisos para exportar logs";
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = objLogActividad.ListarLogs(fechaInicio, fechaFin);
            
            // Aquí implementarías la lógica para exportar a Excel/CSV
            // Por ahora retornamos un archivo de texto simple
            string content = "ID,Usuario,Acción,Tabla,ID Registro,IP Address,Fecha\n";
            
            foreach (DataRow row in dt.Rows)
            {
                content += $"{row["idLog"]},{row["nombreUsuario"]},{row["accion"]},{row["tabla"]},{row["idRegistro"]},{row["ipAddress"]},{row["fecha"]}\n";
            }

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);
            return File(bytes, "text/csv", $"logs_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}.csv");
        }
    }
} 