using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema_ventas_polleria.Models;

namespace Sistema_ventas_polleria.Controllers
{
    public class RealizaPedidoController : Controller
    {
        // Creamos las instancias de los servicios
        private readonly logRealizaPedido _pedidoService = new logRealizaPedido();
        private readonly logEstado _estadoService = new logEstado();

        // GET: /RealizaPedido/
        public ActionResult Index()
        {
            // 1. Obtenemos lista completa de pedidos con detalles
            var pedidos = _pedidoService.ListarPedidosConDetalles();
            // 2. Devolvemos la vista con esos datos
            return View(pedidos);
        }

        // GET: /RealizaPedido/Edit/5
        public ActionResult Edit(int id)
        {
            // 1. Buscamos el pedido por su ID
            var pedido = _pedidoService
                            .ListarPedidosConDetalles()
                            .FirstOrDefault(p => p.idPedidoOnline == id);
            if (pedido == null)
                return NotFound();

            // 2. Obtenemos solo los estados activos
            var listaSelect = _estadoService.ListarEstado()
                                .Where(e => e.estado == 1)
                                .Select(e => new SelectListItem
                                {
                                    Value = e.idEstado.ToString(),
                                    Text = e.nombreEstado
                                });

            // 3. Construimos el ViewModel
            var vm = new PedidoEstadoViewModel
            {
                IdPedido = id,
                IdEstadoNuevo = pedido.idEstado,
                Estados = listaSelect
            };

            return View(vm);
        }

        // POST: /RealizaPedido/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PedidoEstadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores, recargamos la lista de estados para el dropdown
                vm.Estados = _estadoService.ListarEstado()
                                  .Where(e => e.estado == 1)
                                  .Select(e => new SelectListItem
                                  {
                                      Value = e.idEstado.ToString(),
                                      Text = e.nombreEstado
                                  });
                return View(vm);
            }

            // Intentamos actualizar el estado
            bool actualizado = _pedidoService.CambiarEstadoPedido(vm.IdPedido, vm.IdEstadoNuevo);
            if (!actualizado)
            {
                ModelState.AddModelError("", "No se pudo actualizar el estado.");
                // Recargamos la lista de estados
                vm.Estados = _estadoService.ListarEstado()
                                  .Where(e => e.estado == 1)
                                  .Select(e => new SelectListItem
                                  {
                                      Value = e.idEstado.ToString(),
                                      Text = e.nombreEstado
                                  });
                return View(vm);
            }

            // Redirigimos de vuelta al listado
            return RedirectToAction("Index");
        }
    }
}
