using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logMetodoDePago
    {
        #region singleton
        private static readonly logMetodoDePago UnicaInstancia = new logMetodoDePago();
        public static logMetodoDePago Instancia
        {
            get

            {
                return logMetodoDePago.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entMetodoDePago> ListarMetodoDePago()

        {
            List<entMetodoDePago> lista = datMetodoDePago.Instancia.ListarMetodoDePago();
            return lista;
        }
        public Boolean InsertarMetodoDePago(entMetodoDePago c)
        {
            try

            {
                return datMetodoDePago.Instancia.InsertarMetodoDePago(c);
            }
            catch (Exception)

            { throw; }
        }
        public Boolean EditarMetodoDePago(entMetodoDePago c)
        {
            try
            {
                return datMetodoDePago.Instancia.EditarMetodoDePago(c);
            }
            catch (Exception) { throw; }
        }
        public entMetodoDePago BuscarMetodoDePago(int idMetodoPago)
        {
            try
            {
                return datMetodoDePago.Instancia.BuscarMetodoDePago(idMetodoPago);
            }
            catch (Exception) { throw; }
        }
        public Boolean DeshabilitarMetodoDePago(entMetodoDePago c)
        {
            try
            {
                return datMetodoDePago.Instancia.DeshabilitarMetodoDePago(c);
            }
            catch (Exception) { throw; }
        }
        #endregion metodos
    }
}
