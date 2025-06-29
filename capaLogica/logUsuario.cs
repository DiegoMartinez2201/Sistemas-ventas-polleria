using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logUsuario
    {
        #region singleton
        private static readonly logUsuario UnicaInstancia = new logUsuario();
        public static logUsuario Instancia
        {
            get

            {
                return logUsuario.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entUsuario> ListarUsuario()

        {
            List<entUsuario> lista = datUsuario.Instancia.ListarUsuario();
            return lista;
        }
        public Boolean InsertarUsuario(entUsuario c)
        {
            try

            {
                return datUsuario.Instancia.InsertarUsuario(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaUsuario(entUsuario c)
        {
            try
            {
                return datUsuario.Instancia.EditaUsuario(c);
            }
            catch (Exception e) { throw e; }
        }
        public entUsuario BuscarUsuario(int idUsuario)
        {
            try
            {
                return datUsuario.Instancia.BuscarUsuario(idUsuario);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarUsuario(entUsuario c)
        {
            try
            {
                return datUsuario.Instancia.DeshabilitarUsuario(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
