using capa;
using capaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaLogica
{
    public class logTamaño
    {
        #region singleton
        private static readonly logTamaño UnicaInstancia = new logTamaño();
        public static logTamaño Instancia
        {
            get

            {
                return logTamaño.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entTamaño> ListarTamaño()

        {
            List<entTamaño> lista = datTamaño.Instancia.ListarTamaño();
            return lista;
        }
        public Boolean InsertarTamaño(entTamaño c)
        {
            try

            {
                return datTamaño.Instancia.InsertarTamaño(c);
            }
            catch (Exception)

            { throw; }
        }
        public Boolean EditarTamaño(entTamaño c)
        {
            try
            {
                return datTamaño.Instancia.EditarTamaño(c);
            }
            catch (Exception) { throw; }
        }
        public entTamaño BuscarTamaño(int idTamaño)
        {
            try
            {
                return datTamaño.Instancia.BuscarTamaño(idTamaño);
            }
            catch (Exception) { throw; }
        }
        public Boolean DeshabilitarTamaño(entTamaño c)
        {
            try
            {
                return datTamaño.Instancia.DeshabilitarTamaño(c);
            }
            catch (Exception) { throw; }
        }
        #endregion metodos
    }
}
