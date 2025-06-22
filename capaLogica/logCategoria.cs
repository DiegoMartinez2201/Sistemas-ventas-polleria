using capa;
using capaDatos;
using capaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logCategoria
    {
        #region singleton
        private static readonly logCategoria UnicaInstancia = new logCategoria();
        public static logCategoria Instancia
        {
            get

            {
                return logCategoria.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entCategoria> ListarCategoria()

        {
            List<entCategoria> lista = datCategoria.Instancia.ListarCategoria();
            return lista;
        }
        public Boolean InsertarCategoria(entCategoria c)
        {
            try

            {
                return datCategoria.Instancia.InsertarCategoria(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditarCategoria(entCategoria c)
        {
            try
            {
                return datCategoria.Instancia.EditarCategoria(c);
            }
            catch (Exception e) { throw e; }
        }
        public entCategoria BuscarCategoria(int idCategoria)
        {
            try
            {
                return datCategoria.Instancia.BuscarCategoria(idCategoria);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarCategoria(entCategoria c)
        {
            try
            {
                return datCategoria.Instancia.DeshabilitarCategoria(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
