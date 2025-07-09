using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sistema_ventas_polleria.Controllers
{
    public class DetalleComboProducto : Controller
    {
        public IActionResult ListarComboProducto(int idCombo)
        {
            var lista = logComboProducto.Instancia.ListarComboProducto(idCombo);
            ViewBag.idCombo = idCombo; // Por si quieres usarlo en la vista (por ejemplo, para un botón de agregar)
            
            // Obtener información del combo
            var combo = logCombo.Instancia.BuscarCombo(idCombo);
            ViewBag.NombreCombo = combo?.nombreCombo ?? "Combo";
            
            // Obtener información de productos para mostrar nombres
            var productos = logProducto.Instancia.ListarProducto();
            ViewBag.Productos = productos.ToDictionary(p => p.idProducto, p => p.nombreProducto);
            
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarComboProducto(int idCombo)
        {
            // Buscar el nombre del combo
            var combo = logCombo.Instancia.BuscarCombo(idCombo);
            ViewBag.NombreCombo = combo?.nombreCombo ?? "Combo";

            // Cargar productos para el combo box
            List<entProducto> listaProducto = logProducto.Instancia.ListarProducto();
            ViewBag.Productos = new SelectList(listaProducto, "idProducto", "nombreProducto");

            ViewBag.idCombo = idCombo;
            return View();
        }
        [HttpPost]
        public ActionResult InsertarComboProducto(entComboProducto c)
        {
            try
            {
                // Recargar los roles en caso de error de validación
                List<entProducto> listaProducto = logProducto.Instancia.ListarProducto();
                ViewBag.Productos = new SelectList(listaProducto, "idProducto", "nombreProducto");

                Boolean inserta = logComboProducto.Instancia.InsertarComboProducto(c);
                if (inserta)
                {
                    return RedirectToAction("ListarComboProducto");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                // Recargar los roles en caso de excepción
                List<entProducto> listaProductos = logProducto.Instancia.ListarProducto();
                ViewBag.Productos = new SelectList(listaProductos, "idProducto", "nombreProducto");

                return RedirectToAction("InsertarComboProducto", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaComboProducto(int idComboProducto)
        {
            entComboProducto c = new entComboProducto();
            c = logComboProducto.Instancia.BuscarComboProducto(idComboProducto);

            // Cargar los roles para el dropdown
            List<entProducto> listaProductos = logProducto.Instancia.ListarProducto();
            ViewBag.Productos = new SelectList(listaProductos, "idProducto", "nombreProducto");

            return View(c);
        }
        [HttpPost]
        public ActionResult EditaComboProducto(entComboProducto c)
        {
            try
            {
                // Recargar los roles en caso de error de validación
                List<entProducto> listaProductos = logProducto.Instancia.ListarProducto();
                ViewBag.Productos = new SelectList(listaProductos, "idProducto", "nombreProducto");

                Boolean edita = logComboProducto.Instancia.EditarComboProducto(c);
                if (edita)
                {
                    // Redirigir a ListarComboProducto pasando el idCombo
                    return RedirectToAction("ListarComboProducto", new { idCombo = c.idCombo });
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                // Recargar los roles en caso de excepción
                List<entProducto> listaProductos = logProducto.Instancia.ListarProducto();
                ViewBag.Productos = new SelectList(listaProductos, "idProducto", "nombreProducto");

                return RedirectToAction("EditaComboProducto", new { msjExceptio = ex.Message });
            }
        }
       
        [HttpGet]
        public ActionResult Delete(int idComboProducto)
        {
            entComboProducto c = logComboProducto.Instancia.BuscarComboProducto(idComboProducto);
            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int idComboProducto)
        {
            try
            {
                entComboProducto c = logComboProducto.Instancia.BuscarComboProducto(idComboProducto);
                if (c == null)
                {
                    return NotFound();
                }

                Boolean elimina = logComboProducto.Instancia.EliminarComboProducto(idComboProducto);
                if (elimina)
                {
                    return RedirectToAction("ListarComboProducto", new { idCombo = c.idCombo });
                }
                else
                {
                    return RedirectToAction("ListarComboProducto", new { idCombo = c.idCombo, error = "No se pudo eliminar el detalle" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ListarComboProducto", new { error = ex.Message });
            }
        }
    }
}
