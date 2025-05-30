using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entProducto
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public Boolean estado { get; set; }
    }
}
