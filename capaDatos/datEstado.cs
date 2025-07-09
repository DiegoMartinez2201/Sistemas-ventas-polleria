using capa;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public  class datEstado
    {
        #region singleton

        private static readonly datEstado UnicaInstancia = new datEstado();

        public static datEstado Instancia

        {
            get

            {
                return datEstado.UnicaInstancia;

            }

        }

        #endregion singleton
        #region metodos
        public List<entEstado> ListarEstado()
        {
            SqlCommand cmd = null;
            List<entEstado> lista = new List<entEstado>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarEstado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entEstado c = new entEstado();
                    c.idEstado = Convert.ToInt32(dr["idEstado"]);
                    c.nombreEstado = Convert.ToString(dr["nombreEstado"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    lista.Add(c);
                }
            }
            catch (SqlException)
            { throw; }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }

        public Boolean InsertarEstado(entEstado c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarEstado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreEstado", c.nombreEstado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                { inserta = true; }
            }
            catch (SqlException) { throw; }
            finally { cmd.Connection.Close(); }
            return inserta;
        }
        public Boolean EditarEstado(entEstado c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaEstado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idEstado", c.idEstado);
                cmd.Parameters.AddWithValue("@nombreEstado", c.nombreEstado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { edita = true; }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entEstado BuscarEstado(int idEstado)
        {
            SqlCommand cmd = null;
            entEstado c = new entEstado();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarEstado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idEstado", idEstado);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idEstado = Convert.ToInt32(dr["idEstado"]);
                    c.nombreEstado = Convert.ToString(dr["nombreEstado"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return c;
        }
        public Boolean DeshabilitarEstado(entEstado c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarEstado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idEstado", c.idEstado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    delete = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close(); }
            return delete;
        }
        #endregion metodos


    }
}
