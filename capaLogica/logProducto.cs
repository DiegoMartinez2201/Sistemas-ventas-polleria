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
        #endregion metodos

    }
}
