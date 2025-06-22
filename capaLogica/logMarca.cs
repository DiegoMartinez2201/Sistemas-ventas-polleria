using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logMarca
    {
        #region singleton
        private static readonly logMarca UnicaInstancia = new logMarca();
        public static logMarca Instancia
        {
            get

            {
                return logMarca.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entMarca> ListarMarca()

        {
            List<entMarca> lista = datMarca.Instancia.ListarMarca();
            return lista;
        }
        public Boolean InsertarMarca(entMarca c)
        {
            try

            {
                return datMarca.Instancia.InsertarMarca(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditarMarca(entMarca c)
        {
            try
            {
                return datMarca.Instancia.EditarMarca(c);
            }
            catch (Exception e) { throw e; }
        }
        public entMarca BuscarMarca(int idMarca)
        {
            try
            {
                return datMarca.Instancia.BuscarMarca(idMarca);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarMarca(entMarca c)
        {
            try
            {
                return datMarca.Instancia.DeshabilitarMarca(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
