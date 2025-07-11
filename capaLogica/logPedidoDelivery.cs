using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logPedidoDelivery
    {
         private static readonly logPedidoDelivery _instancia = new logPedidoDelivery();
         public static logPedidoDelivery Instancia { get { return _instancia; } }



        // Método para listar pedidos por cliente
        /*public List<entPedidoDelivery> ListarPedidosPorCliente(int idCliente)
        {
            return datPedidoDelivery.Instancia.ListarPedidosPorCliente(idCliente);
        }*/
        // Método para actualizar el estado de un pedido


        /*[HttpPost]
        public IActionResult ActualizarEstado(int idPedido, string nuevoEstado)
        {
            // Llama a la lógica para actualizar el estado del pedido
            logPedidoDelivery.Instancia.ActualizarEstadoPedido(idPedido, nuevoEstado);

            // Redirige de nuevo a la lista de pedidos
            return RedirectToAction("ListarPedido");
        }
        */
    }

}
