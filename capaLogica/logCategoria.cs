using capa;
using capaDatos;
using System;
using System.Collections.Generic;

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
            return datCategoria.Instancia.ListarCategoria();
        }

        public Boolean InsertarCategoria(entCategoria c)
        {
            try
            {
                return datCategoria.Instancia.InsertarCategoria(c);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean EditarCategoria(entCategoria c)
        {
            try
            {
                return datCategoria.Instancia.EditarCategoria(c);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public entCategoria BuscarCategoria(int idCategoria)
        {
            try
            {
                return datCategoria.Instancia.BuscarCategoria(idCategoria);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean DeshabilitarCategoria(entCategoria c)
        {
            try
            {
                return datCategoria.Instancia.DeshabilitarCategoria(c);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion metodos
    }
}
