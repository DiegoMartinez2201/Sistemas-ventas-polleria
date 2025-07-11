using capa;
using capaLogica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Sistema_ventas_polleria.Controllers
{
    public class MantenedorPedidosDeliveryController : Controller
    {
        public IActionResult ListarPedido()
        {

            //Asegúrarse de que "IdCliente" esté guardado en la sesión al iniciar sesión
            /*int idCliente = Convert.ToInt32(HttpContext.Session.GetString("IdCliente"));
            var pedidos = logPedidoDelivery.Instancia.ListarPedidosPorCliente(idCliente);
            return View(pedidos); 
           /* List<entPedidoDelivery> lista = logPedidoDelivery.Instancia.ListarPedidosPorCliente(idCliente);
            ViewBag.lista = lista;
            return View(lista);*/
            return View();

            /*[HttpPost]
            public IActionResult ActualizarEstado(int idPedido, string nuevoEstado)
            {
                // Llama a la lógica para actualizar el estado del pedido
                logPedidoDelivery.Instancia.ActualizarEstadoPedido(idPedido, nuevoEstado);

                // Redirige de nuevo a la lista de pedidos
                return RedirectToAction("ListarPedido");
            */
            
        }
    }
}


