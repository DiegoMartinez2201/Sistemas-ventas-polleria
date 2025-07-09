using System;
using System.Data;
using capaDatos;
using capa;

namespace capaLogica
{
    public class logLogActividad
    {
        private datLogActividad objDatLog = new datLogActividad();

        public string RegistrarActividad(entLogActividad log)
        {
            // Validaciones de negocio
            if (string.IsNullOrEmpty(log.accion))
                return "La acción es obligatoria";

            if (log.fecha == DateTime.MinValue)
                log.fecha = DateTime.Now;

            return objDatLog.RegistrarActividad(log);
        }

        public DataTable ListarLogs(DateTime fechaInicio, DateTime fechaFin)
        {
            // Validaciones de negocio
            if (fechaInicio > fechaFin)
                return new DataTable();

            if (fechaInicio < DateTime.Now.AddYears(-1))
                fechaInicio = DateTime.Now.AddYears(-1);

            return objDatLog.ListarLogs(fechaInicio, fechaFin);
        }

        public string LimpiarLogsAntiguos(int diasRetener = 90)
        {
            // Validaciones de negocio
            if (diasRetener < 1)
                return "Los días a retener deben ser mayor a 0";

            if (diasRetener > 365)
                return "Los días a retener no pueden exceder 365 días";

            return objDatLog.LimpiarLogsAntiguos(diasRetener);
        }

        public string RegistrarActividadSimple(int? idUsuario, string accion, string tabla = null, int? idRegistro = null, string ipAddress = null)
        {
            var log = new entLogActividad
            {
                idUsuario = idUsuario,
                accion = accion,
                tabla = tabla,
                idRegistro = idRegistro,
                ipAddress = ipAddress,
                fecha = DateTime.Now
            };

            return RegistrarActividad(log);
        }
    }
} 