using System;

namespace capaEntidad
{
    public class entSesionUsuario
    {
        public int idSesion { get; set; }
        public int idUsuario { get; set; }
        public string token { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaExpiracion { get; set; }
        public string ipAddress { get; set; }
        public string userAgent { get; set; }
        public bool activa { get; set; }
    }
} 