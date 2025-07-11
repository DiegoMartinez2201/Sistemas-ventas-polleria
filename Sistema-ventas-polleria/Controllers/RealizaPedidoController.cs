using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema_ventas_polleria.Models;

namespace Sistema_ventas_polleria.Controllers
{
    public class RealizaPedidoController : Controller
    {
        private readonly logRealizaPedido _pedidoService = new logRealizaPedido();
        private readonly logEstado _estadoService = new logEstado();

        // GET: /RealizaPedido/Index
        public IActionResult Index()
        {
            var pedidos = _pedidoService.ListarPedidosConDetalles();

            // Si es llamada AJAX, devolvemos solo el partial _Listado
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_Listado", pedidos);
            }

            // Llamada normal: renderizamos Index.cshtml que a su vez incluye el partial
            return View(pedidos);
        }

        // GET: /RealizaPedido/Edit/5
        public IActionResult Edit(int id)
        {
            var pedido = _pedidoService
                            .ListarPedidosConDetalles()
                            .FirstOrDefault(p => p.idPedidoOnline == id);
            if (pedido == null)
                return NotFound();

            var vm = new PedidoEstadoViewModel
            {
                IdPedido = id,
                IdEstadoNuevo = pedido.idEstado,
                Estados = _estadoService.ListarEstado()
                                 .Where(e => e.estado == 1)
                                 .Select(e => new SelectListItem
                                 {
                                     Value = e.idEstado.ToString(),
                                     Text = e.nombreEstado
                                 }).ToList()
            };

            // Si viene por AJAX, devolvemos sólo el partial:
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_Edit", vm);

            // Si es una navegación directa, devolvemos la vista completa
            return View("Edit", vm);
        }

        // POST: /RealizaPedido/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(PedidoEstadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // validación fallida: devolvemos el formulario con errores
                vm.Estados = _estadoService.ListarEstado()
                                  .Where(e => e.estado == 1)
                                  .Select(e => new SelectListItem
                                  {
                                      Value = e.idEstado.ToString(),
                                      Text = e.nombreEstado
                                  }).ToList();
                return PartialView("_Edit", vm);
            }

            bool updated = _pedidoService.CambiarEstadoPedido(vm.IdPedido, vm.IdEstadoNuevo);
            if (!updated)
            {
                ModelState.AddModelError("", "No se pudo actualizar el estado.");
                vm.Estados = _estadoService.ListarEstado()
                                  .Where(e => e.estado == 1)
                                  .Select(e => new SelectListItem
                                  {
                                      Value = e.idEstado.ToString(),
                                      Text = e.nombreEstado
                                  }).ToList();
                return PartialView("_Edit", vm);
            }

            // Éxito: devolvemos JSON para que el cliente recargue el listado
            return Json(new { success = true });
        }
    }
}
