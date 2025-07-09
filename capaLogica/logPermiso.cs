using System;
using System.Data;
using capaDatos;
using capaEntidad;

namespace capaLogica
{
    public class logPermiso
    {
        private datPermiso objDatPermiso = new datPermiso();

        public DataTable ListarPermisos()
        {
            return objDatPermiso.ListarPermisos();
        }

        public DataTable BuscarPermiso(int idPermiso)
        {
            return objDatPermiso.BuscarPermiso(idPermiso);
        }

        public string InsertarPermiso(entPermiso permiso)
        {
            // Validaciones de negocio
            if (string.IsNullOrEmpty(permiso.nombrePermiso))
                return "El nombre del permiso es obligatorio";

            if (permiso.nombrePermiso.Length > 100)
                return "El nombre del permiso no puede exceder 100 caracteres";

            return objDatPermiso.InsertarPermiso(permiso);
        }

        public string EditarPermiso(entPermiso permiso)
        {
            // Validaciones de negocio
            if (permiso.idPermiso <= 0)
                return "ID de permiso inválido";

            if (string.IsNullOrEmpty(permiso.nombrePermiso))
                return "El nombre del permiso es obligatorio";

            if (permiso.nombrePermiso.Length > 100)
                return "El nombre del permiso no puede exceder 100 caracteres";

            return objDatPermiso.EditarPermiso(permiso);
        }

        public string DeshabilitarPermiso(int idPermiso)
        {
            if (idPermiso <= 0)
                return "ID de permiso inválido";

            return objDatPermiso.DeshabilitarPermiso(idPermiso);
        }
    }
} 