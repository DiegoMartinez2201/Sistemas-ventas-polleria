using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorProducto : Controller
    {
        public IActionResult ListarProducto()
        {
            List<entProducto> lista = logProducto.Instancia.ListarProducto();
            ViewBag.lista = lista;
            return View(lista);
        }
        [HttpGet]
        public ActionResult InsertarProducto()
        {
            // Cargar los categoria,marcas y tamaños desde la base de datos
            List<entCategoria> listaCategorias = logCategoria.Instancia.ListarCategoria();
            List<entMarca> listaMarcas = logMarca.Instancia.ListarMarca();
            List<entTamaño> listaTamaños = logTamaño.Instancia.ListarTamaño();

            // Crear SelectList para el dropdown de Categorias, Marcas y Tamaños
            ViewBag.Categorias = new SelectList(listaCategorias, "idCategoria", "nombreCategoria");
            ViewBag.Marcas = new SelectList(listaMarcas, "idMarca", "nombreMarca");
            ViewBag.Tamaños = new SelectList(listaTamaños, "idTamaño", "nombre");

            return View();
        }
        [HttpPost]
        public ActionResult InsertarProducto(entProducto c)
        {
            try
            {
                // Recargar los roles en caso de error de validación
                List<entCategoria> listaCategorias = logCategoria.Instancia.ListarCategoria();
                List<entMarca> listaMarcas = logMarca.Instancia.ListarMarca();
                List<entTamaño> listaTamaños = logTamaño.Instancia.ListarTamaño();

                // Crear SelectList para el dropdown de Categorias, Marcas y Tamaños
                ViewBag.Categorias = new SelectList(listaCategorias, "idCategoria", "nombreCategoria");
                ViewBag.Marcas = new SelectList(listaMarcas, "idMarca", "nombreMarca");
                ViewBag.Tamaños = new SelectList(listaTamaños, "idTamaño", "nombre");

                Boolean inserta = logProducto.Instancia.InsertarProducto(c);
                if (inserta)
                {
                    return RedirectToAction("ListarProducto");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                // Recargar los roles en caso de excepción
                List<entCategoria> listaCategorias = logCategoria.Instancia.ListarCategoria();
                List<entMarca> listaMarcas = logMarca.Instancia.ListarMarca();
                List<entTamaño> listaTamaños = logTamaño.Instancia.ListarTamaño();

                // Crear SelectList para el dropdown de Categorias, Marcas y Tamaños
                ViewBag.Categorias = new SelectList(listaCategorias, "idCategoria", "nombreCategoria");
                ViewBag.Marcas = new SelectList(listaMarcas, "idMarca", "nombreMarca");
                ViewBag.Tamaños = new SelectList(listaTamaños, "idTamaño", "nombre");

                return RedirectToAction("InsertarProducto", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult EditaProducto(int idProducto)
        {
            entProducto c = new entProducto();
            c = logProducto.Instancia.BuscarProducto(idProducto);

            // Cargar los roles para el dropdown
            List<entCategoria> listaCategorias = logCategoria.Instancia.ListarCategoria();
            List<entMarca> listaMarcas = logMarca.Instancia.ListarMarca();
            List<entTamaño> listaTamaños = logTamaño.Instancia.ListarTamaño();

            // Crear SelectList para el dropdown de Categorias, Marcas y Tamaños
            ViewBag.Categorias = new SelectList(listaCategorias, "idCategoria", "nombreCategoria");
            ViewBag.Marcas = new SelectList(listaMarcas, "idMarca", "nombreMarca");
            ViewBag.Tamaños = new SelectList(listaTamaños, "idTamaño", "nombre");

            return View(c);
        }
        [HttpPost]
        public ActionResult EditaProducto(entProducto c)
        {
            try
            {
                // Recargar los roles en caso de error de validación
                List<entCategoria> listaCategorias = logCategoria.Instancia.ListarCategoria();
                List<entMarca> listaMarcas = logMarca.Instancia.ListarMarca();
                List<entTamaño> listaTamaños = logTamaño.Instancia.ListarTamaño();

                // Crear SelectList para el dropdown de Categorias, Marcas y Tamaños
                ViewBag.Categorias = new SelectList(listaCategorias, "idCategoria", "nombreCategoria");
                ViewBag.Marcas = new SelectList(listaMarcas, "idMarca", "nombreMarca");
                ViewBag.Tamaños = new SelectList(listaTamaños, "idTamaño", "nombre");

                Boolean edita = logProducto.Instancia.EditaProducto(c);
                if (edita)
                {
                    return RedirectToAction("ListarProducto");
                }
                else
                {
                    return View(c);
                }
            }
            catch (ApplicationException ex)
            {
                // Recargar los roles en caso de excepción
                List<entCategoria> listaCategorias = logCategoria.Instancia.ListarCategoria();
                List<entMarca> listaMarcas = logMarca.Instancia.ListarMarca();
                List<entTamaño> listaTamaños = logTamaño.Instancia.ListarTamaño();

                // Crear SelectList para el dropdown de Categorias, Marcas y Tamaños
                ViewBag.Categorias = new SelectList(listaCategorias, "idCategoria", "nombreCategoria");
                ViewBag.Marcas = new SelectList(listaMarcas, "idMarca", "nombreMarca");
                ViewBag.Tamaños = new SelectList(listaTamaños, "idTamaño", "nombre");

                return RedirectToAction("EditaProducto", new { msjExceptio = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult DeshabilitaProducto(int idProducto)
        {
            entProducto producto = logProducto.Instancia.BuscarProducto(idProducto);
            return View(producto);
        }
        [HttpPost]
        public ActionResult ConfirmarDeshabilitarProducto(int idProducto)
        {
            try
            {
                entProducto c = new entProducto { idProducto = idProducto };
                bool resultado = logProducto.Instancia.DeshabilitarProducto(c);
                if (resultado)
                {
                    return RedirectToAction("ListarProducto");
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
