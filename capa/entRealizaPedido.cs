using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entRealizaPedido
    {
        public int idPedidoOnline { get; set; }
        public int idUsuario { get; set; }
        public int idEstado { get; set; }
        public int idFormaEnvio { get; set; }
        public int idMetodoPago { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public string observaciones { get; set; }
        public string direccion { get; set; }

        // — Navegación a las entidades maestras —

        /// <summary>Datos del usuario que hizo el pedido.</summary>
        public entUsuario Usuario { get; set; }

        /// <summary>Estado actual del pedido.</summary>
        public entEstado Estado { get; set; }

        /// <summary>Forma de envío elegida.</summary>
        public entFormaEnvio FormaEnvio { get; set; }

        /// <summary>Método de pago seleccionado.</summary>
        public entMetodoDePago MetodoPago { get; set; }

        // — Detalles del pedido —

        /// <summary>Si el pedido incluye combos.</summary>
        public List<entPedidoCombo> Combos { get; set; }

        /// <summary>Si el pedido incluye productos sueltos.</summary>
        public List<entPedidoProducto> Productos { get; set; }
    }
}
