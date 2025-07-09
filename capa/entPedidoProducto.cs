using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entPedidoProducto
    {
        public int idPedidoProducto { get; set; }   
        public int idPedidoOnline { get; set; }
        public int idProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }

    }
}
