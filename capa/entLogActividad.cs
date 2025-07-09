using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entLogActividad
    {
        public int idLog { get; set; }
        public int? idUsuario { get; set; }
        public string accion { get; set; }
        public string tabla { get; set; }
        public int? idRegistro { get; set; }
        public string datosAnteriores { get; set; }
        public string datosNuevos { get; set; }
        public string ipAddress { get; set; }
        public System.DateTime fecha { get; set; }
    }
} 