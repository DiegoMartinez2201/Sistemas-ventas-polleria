using System;
using System.Data;
using capaDatos;
using capaEntidad;

namespace capaLogica
{
    public class logSesionUsuario
    {
        private datSesionUsuario objDatSesion = new datSesionUsuario();

        public string CrearSesion(entSesionUsuario sesion)
        {
            // Validaciones de negocio
            if (sesion.idUsuario <= 0)
                return "ID de usuario inválido";

            if (string.IsNullOrEmpty(sesion.token))
                return "Token de sesión es obligatorio";

            if (sesion.fechaExpiracion <= DateTime.Now)
                return "La fecha de expiración debe ser mayor a la fecha actual";

            return objDatSesion.CrearSesion(sesion);
        }

        public DataTable ValidarSesion(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new DataTable();

            return objDatSesion.ValidarSesion(token);
        }

        public string CerrarSesion(string token)
        {
            if (string.IsNullOrEmpty(token))
                return "Token de sesión es obligatorio";

            return objDatSesion.CerrarSesion(token);
        }

        public bool VerificarPermiso(int idUsuario, string nombrePermiso)
        {
            if (idUsuario <= 0)
                return false;

            if (string.IsNullOrEmpty(nombrePermiso))
                return false;

            return objDatSesion.VerificarPermiso(idUsuario, nombrePermiso);
        }

        public string GenerarToken()
        {
            return Guid.NewGuid().ToString();
        }

        public DateTime CalcularExpiracion(int horas = 24)
        {
            return DateTime.Now.AddHours(horas);
        }
    }
} 