using capaEntidad;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logTipoCliente
    {
        #region singleton
        private static readonly logTipoCliente UnicaInstancia = new logTipoCliente();
        public static logTipoCliente Instancia
        {
            get

            {
                return logTipoCliente.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entTipoCliente> ListarTipoCliente()

        {
            List<entTipoCliente> lista = datTipoCliente.Instancia.ListarTipoCliente();
            return lista;
        }
        public Boolean InsertarTipoCliente(entTipoCliente c)
        {
            try

            {
                return datTipoCliente.Instancia.InsertarTipoCliente(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaTipoCliente(entTipoCliente c)
        {
            try
            {
                return datTipoCliente.Instancia.EditaTipoCliente(c);
            }
            catch (Exception e) { throw e; }
        }
        public entTipoCliente BuscarTipoCliente(int idTipoCliente)
        {
            try
            {
                return datTipoCliente.Instancia.BuscarTipoCliente(idTipoCliente);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarTipoCliente(entTipoCliente c)
        {
            try
            {
                return datTipoCliente.Instancia.DeshabilitarTipoCliente(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
