using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logEstado
    {
        #region singleton
        private static readonly logEstado UnicaInstancia = new logEstado();
        public static logEstado Instancia
        {
            get

            {
                return logEstado.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entEstado> ListarEstado()

        {
            List<entEstado> lista = datEstado.Instancia.ListarEstado();
            return lista;
        }
        public Boolean InsertarEstado(entEstado c)
        {
            try

            {
                return datEstado.Instancia.InsertarEstado(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaEstado(entEstado c)
        {
            try
            {
                return datEstado.Instancia.EditarEstado(c);
            }
            catch (Exception e) { throw e; }
        }
        public entEstado BuscarEstado(int idEstado)
        {
            try
            {
                return datEstado.Instancia.BuscarEstado(idEstado);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarEstado(entEstado c)
        {
            try
            {
                return datEstado.Instancia.DeshabilitarEstado(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
