using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entComprobanteDePago
    {
        public int idComprobantePago { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public decimal monto { get; set; }
        public int idPedidoOnline { get; set; }

    }
}
