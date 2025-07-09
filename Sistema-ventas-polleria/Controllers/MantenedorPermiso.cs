using Microsoft.AspNetCore.Mvc;
using capaLogica;
using capaEntidad;
using System.Data;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorPermiso : Controller
    {
        private logPermiso objLogPermiso = new logPermiso();

        public IActionResult Index()
        {
            DataTable dt = objLogPermiso.ListarPermisos();
            return View(dt);
        }

        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(entPermiso permiso)
        {
            string mensaje = objLogPermiso.InsertarPermiso(permiso);
            if (mensaje == "OK")
            {
                TempData["Mensaje"] = "Permiso insertado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = mensaje;
                return View(permiso);
            }
        }

        public IActionResult Editar(int id)
        {
            DataTable dt = objLogPermiso.BuscarPermiso(id);
            if (dt.Rows.Count > 0)
            {
                entPermiso permiso = new entPermiso
                {
                    idPermiso = Convert.ToInt32(dt.Rows[0]["idPermiso"]),
                    nombrePermiso = dt.Rows[0]["nombrePermiso"].ToString(),
                    descripcion = dt.Rows[0]["descripcion"].ToString(),
                    estado = Convert.ToInt32(dt.Rows[0]["estado"])
                };
                return View(permiso);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Editar(entPermiso permiso)
        {
            string mensaje = objLogPermiso.EditarPermiso(permiso);
            if (mensaje == "OK")
            {
                TempData["Mensaje"] = "Permiso editado correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = mensaje;
                return View(permiso);
            }
        }

        public IActionResult Deshabilitar(int id)
        {
            string mensaje = objLogPermiso.DeshabilitarPermiso(id);
            if (mensaje == "OK")
            {
                TempData["Mensaje"] = "Permiso deshabilitado correctamente";
            }
            else
            {
                TempData["Error"] = mensaje;
            }
            return RedirectToAction("Index");
        }
    }
} 