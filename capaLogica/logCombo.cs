using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logCombo
    {
        #region singleton
        private static readonly logCombo UnicaInstancia = new logCombo();
        public static logCombo Instancia
        {
            get

            {
                return logCombo.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entCombo> ListarCombo()

        {
            List<entCombo> lista = datCombo.Instancia.ListarCombo();
            return lista;
        }
        public Boolean InsertarCombo(entCombo c)
        {
            try

            {
                return datCombo.Instancia.InsertarCombo(c);
            }
            catch (Exception e)

            { throw e; }
        }
        public Boolean EditaCombo(entCombo c)
        {
            try
            {
                return datCombo.Instancia.EditaCombo(c);
            }
            catch (Exception e) { throw e; }
        }
        public entCombo BuscarCombo(int idCombo)
        {
            try
            {
                return datCombo.Instancia.BuscarCombo(idCombo);
            }
            catch (Exception e) { throw e; }
        }
        public Boolean DeshabilitarCombo(entCombo c)
        {
            try
            {
                return datCombo.Instancia.DeshabilitarCombo(c);
            }
            catch (Exception e) { throw e; }
        }
        #endregion metodos
    }
}
