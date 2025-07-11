using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logRealizaPedido
    {
        /// <summary>
        /// Obtiene todos los pedidos en línea, con sus datos maestros y detalles (combos y productos).
        /// </summary>
        public List<entRealizaPedido> ListarPedidosConDetalles()
        {
            // Delegamos a la capa de datos
            return datRealizaPedido.Instancia.ListarTodosConDetalles();
        }

        /// <summary>
        /// Cambia el estado de un pedido específico.
        /// </summary>
        /// <param name="idPedidoOnline">Identificador de RealizaPedido.</param>
        /// <param name="nuevoEstado">El nuevo idEstado que se desea asignar.</param>
        /// <returns>True si se actualizó correctamente.</returns>
        public bool CambiarEstadoPedido(int idPedidoOnline, int nuevoEstado)
        {
            // Podrías agregar aquí validaciones de reglas de negocio
            return datRealizaPedido.Instancia.ActualizarEstado(idPedidoOnline, nuevoEstado);
        }
    }
}
