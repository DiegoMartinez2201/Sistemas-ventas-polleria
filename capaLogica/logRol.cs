using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logRol
    {
        #region singleton
        private static readonly logRol UnicaInstancia = new logRol();
        public static logRol Instancia
        {
            get

            {
                return logRol.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entRol> ListarRol()

        {
            List<entRol> lista = datRol.Instancia.ListarRol();
            return lista;
        }
        public Boolean InsertarRol(entRol c)
        {
            try

            {
                return datRol.Instancia.InsertarRol(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaRol(entRol c)
        {
            try
            {
                return datRol.Instancia.EditaRol(c);
            }
            catch (Exception e) { throw e; }
        }
        public entRol BuscarRol(int idRol)
        {
            try
            {
                return datRol.Instancia.BuscarRol(idRol);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarRol(entRol c)
        {
            try
            {
                return datRol.Instancia.DeshabilitarRol(c);
            }
            catch (Exception e) { throw e; }
        }
        public List<entRol> ListarTodosRoles()
        {
            return datRol.Instancia.ListarTodosRoles();
        }
        #endregion metodos
    }
}
