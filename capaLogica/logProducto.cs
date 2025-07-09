using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logProducto
    {
        #region singleton
        private static readonly logProducto UnicaInstancia = new logProducto();
        public static logProducto Instancia
        {
            get

            {
                return logProducto.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entProducto> ListarProducto()

        {
            List<entProducto> lista = datProducto.Instancia.ListarProducto();
            return lista;
        }
        public Boolean InsertarProducto(entProducto c)
        {
            try

            {
                return datProducto.Instancia.InsertarProducto(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaProducto(entProducto c)
        {
            try
            {
                return datProducto.Instancia.EditaProducto(c);
            }
            catch (Exception e) { throw e; }
        }
        public entProducto BuscarProducto(int idProducto)
        {
            try
            {
                return datProducto.Instancia.BuscarProducto(idProducto);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarProducto(entProducto c)
        {
            try
            {
                return datProducto.Instancia.DeshabilitarProducto(c);
            }
            catch (Exception e) { throw e; }
        }
        public List<entProducto> ListarProductoPorCategoria(int idCategoria)
        {
            return datProducto.Instancia.ListarProductoPorCategoria(idCategoria);
        }
        #endregion metodos
    }
}
