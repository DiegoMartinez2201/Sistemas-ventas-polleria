using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entPedidoOnline
    {
        public int idPedidoOnline { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora { get; set; }
        public int idFormaEnvio { get; set; }
        public int idMetodoPago { get; set; }
        public string observaciones { get; set; }   
        public int idEstado { get; set; }
        public int idCliente { get; set; }
       
    }
}
