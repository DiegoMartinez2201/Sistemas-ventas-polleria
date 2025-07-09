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
    public class datFormaEnvio
    {
        #region singleton

        private static readonly datFormaEnvio UnicaInstancia = new datFormaEnvio();

        public static datFormaEnvio Instancia

        {
            get

            {
                return datFormaEnvio.UnicaInstancia;

            }

        }

        #endregion singleton
        #region metodos

        public List<entFormaEnvio> ListarFormaEnvio()

        {
            SqlCommand cmd = null;
            List<entFormaEnvio> lista = new List<entFormaEnvio>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarFormaEnvio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entFormaEnvio c = new entFormaEnvio();
                    c.idFormaEnvio = Convert.ToInt32(dr["idFormaEnvio"]);
                    c.nombreForma = Convert.ToString(dr["nombreForma"]);
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
        public Boolean InsertarFormaEnvio(entFormaEnvio c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarFormaEnvio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreForma", c.nombreForma);
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
        public Boolean EditaFormaEnvio(entFormaEnvio c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaFormaEnvio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idFormaEnvio", c.idFormaEnvio);
                cmd.Parameters.AddWithValue("@nombreForma", c.nombreForma);

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
        public entFormaEnvio BuscarFormaEnvio(int idFormaEnvio)
        {
            SqlCommand cmd = null;
            entFormaEnvio c = new entFormaEnvio();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarFormaEnvio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idFormaEnvio", idFormaEnvio);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                { //c = new entCliente();
                    c.idFormaEnvio = Convert.ToInt32(dr["idFormaEnvio"]);
                    c.nombreForma = Convert.ToString(dr["nombreForma"]);
                }
            }
            catch (Exception e)
            { throw e; }
            finally { cmd.Connection.Close(); }
            return c;
        }
        public Boolean DeshabilitarFormaEnvio(entFormaEnvio c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarFormaEnvio", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idFormaEnvio", c.idFormaEnvio);
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
