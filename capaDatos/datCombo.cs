using capa;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public  class datCombo
    {
        #region singleton
        private static readonly datCombo UnicaInstancia = new datCombo();
        public static datCombo Instancia
        {
            get
            {
                return datCombo.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos

        public List<entCombo> ListarCombo()

        {
            SqlCommand cmd = null;
            List<entCombo> lista = new List<entCombo>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarCombo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entCombo c = new entCombo();
                    c.idCombo = Convert.ToInt32(dr["idCombo"]);
                    c.nombreCombo = Convert.ToString(dr["nombreCombo"]);
                    c.descripcion = Convert.ToString(dr["descripcion"]);
                    c.precioVenta = Convert.ToDecimal(dr["precioVenta"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    lista.Add(c);
                }
            }

            catch (SqlException e)
            { throw e; }
            finally
            { cmd.Connection.Close(); }
            return lista;
        }
        public Boolean InsertarCombo(entCombo c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarCombo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreCombo", c.nombreCombo);
                cmd.Parameters.AddWithValue("@descripcion", c.descripcion);
                cmd.Parameters.AddWithValue("@precioVenta", c.precioVenta);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                { inserta = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }

            return inserta;

        }
        public Boolean EditaCombo(entCombo c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaCombo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCombo", c.idCombo);
                cmd.Parameters.AddWithValue("@nombreCombo", c.nombreCombo);
                cmd.Parameters.AddWithValue("@descripcion", c.descripcion);
                cmd.Parameters.AddWithValue("@precioVenta", c.precioVenta);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    edita = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entCombo BuscarCombo(int idCombo)
        {
            SqlCommand cmd = null;
            entCombo c = new entCombo();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarCombo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCombo", idCombo);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                { //c = new entCliente();
                    c.idCombo = Convert.ToInt32(dr["idCombo"]);
                    c.nombreCombo = Convert.ToString(dr["nombreCombo"]);
                    c.descripcion = Convert.ToString(dr["descripcion"]);
                    c.precioVenta = Convert.ToDecimal(dr["precioVenta"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch (Exception e)
            { throw e; }
            finally { cmd.Connection.Close(); }
            return c;
        }
        public Boolean DeshabilitarCombo(entCombo c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarCombo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCombo", c.idCombo);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    delete = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return delete;

        }
        #endregion metodos
    }
}
