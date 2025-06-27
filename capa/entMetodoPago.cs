using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entMetodoPago
    {
        public int idMetodoPago { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; } // 1: activo, 0: inactivo
        
    }
}
