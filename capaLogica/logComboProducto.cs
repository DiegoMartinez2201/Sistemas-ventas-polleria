using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logComboProducto
    {
        #region singleton
        private static readonly logComboProducto UnicaInstancia = new logComboProducto();
        public static logComboProducto Instancia
        {
            get

            {
                return logComboProducto.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entComboProducto> ListarComboProducto(int idCombo)

        {
            List<entComboProducto> lista = datComboProducto.Instancia.ListarComboProducto(idCombo);
            return lista;
        }
        public Boolean InsertarComboProducto(entComboProducto c)
        {
            try

            {
                return datComboProducto.Instancia.InsertarComboProducto(c);
            }
            catch (Exception)

            { throw; }
        }
        public Boolean EditarComboProducto(entComboProducto c)
        {
            try
            {
                return datComboProducto.Instancia.EditaComboProducto(c);
            }
            catch (Exception) { throw; }
        }
        public entComboProducto BuscarComboProducto(int idComboProducto)
        {
            try
            {
                return datComboProducto.Instancia.BuscarComboProducto(idComboProducto);
            }
            catch (Exception) { throw; }
        }
        
        #endregion metodos
    }
}
