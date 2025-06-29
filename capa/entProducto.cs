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
        public int idCategoria { get; set; }    
        public int idMarca { get; set; }
        public int idTamaño { get; set; }
        public string nombreProducto { get; set; }  
        public decimal precioVenta { get; set; }
        public int stock { get; set; }
        public int estado { get; set; } 
        public string descripcionP { get; set; }
    }
}
