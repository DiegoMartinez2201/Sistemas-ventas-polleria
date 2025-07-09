using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa
{
    public class entUsuario
    {
        public int idUsuario { get; set; }
        public string correo { get; set; }
        public string contraseña { get; set; }
        public string dni { get; set; }
        public string nombreCli { get; set; }
        public string apellidoCli { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public int idRol { get; set; }
        public int estado { get; set; }
        
        // Campos de auditoría
        public int? createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
