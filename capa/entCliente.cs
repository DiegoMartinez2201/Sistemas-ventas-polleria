using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entCliente
    {
        public int idCliente { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }  
        public string dni { get; set; }
        public string primernombre { get; set; }
        public string segundonombre { get; set; }   
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }   
        public string telefono { get; set; }
        public int idTipoCliente { get; set; }  
        public int estado { get; set; } 

    }
}
