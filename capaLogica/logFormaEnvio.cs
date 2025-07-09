using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logFormaEnvio
    {
        #region singleton
        private static readonly logFormaEnvio UnicaInstancia = new logFormaEnvio();
        public static logFormaEnvio Instancia
        {
            get

            {
                return logFormaEnvio.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entFormaEnvio> ListarFormaEnvio()

        {
            List<entFormaEnvio> lista = datFormaEnvio.Instancia.ListarFormaEnvio();
            return lista;
        }
        public Boolean InsertarFormaEnvio(entFormaEnvio c)
        {
            try

            {
                return datFormaEnvio.Instancia.InsertarFormaEnvio(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaFormaEnvio(entFormaEnvio c)
        {
            try
            {
                return datFormaEnvio.Instancia.EditaFormaEnvio(c);
            }
            catch (Exception e) { throw e; }
        }
        public entFormaEnvio BuscarFormaEnvio(int idFormaEnvio)
        {
            try
            {
                return datFormaEnvio.Instancia.BuscarFormaEnvio(idFormaEnvio);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarFormaEnvio(entFormaEnvio c)
        {
            try
            {
                return datFormaEnvio.Instancia.DeshabilitarFormaEnvio(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
