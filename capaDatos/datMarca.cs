using capa;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public class datMarca
    {
        #region singleton
        private static readonly datMarca UnicaInstancia = new datMarca();
        public static datMarca Instancia
        {
            get
            {
                return datMarca.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entMarca> ListarMarca()
        {
            SqlCommand cmd = null;
            List<entMarca> lista = new List<entMarca>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarMarca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entMarca c = new entMarca();
                    c.idMarca = Convert.ToInt32(dr["idMarca"]);
                    c.nombreMarca = Convert.ToString(dr["nombreMarca"]);
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

        public Boolean InsertarMarca(entMarca c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarMarca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreMarca", c.nombreMarca);
                cmd.Parameters.AddWithValue("@estado", c.estado);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                { inserta = true; }
            }
            catch (SqlException) { throw; }
            finally { cmd.Connection.Close(); }
            return inserta;
        }
        public Boolean EditarMarca(entMarca c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarMarca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMarca", c.idMarca);
                cmd.Parameters.AddWithValue("@nombreMarca", c.nombreMarca);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { edita = true; }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entMarca BuscarMarca(int idMarca)
        {
            SqlCommand cmd = null;
            entMarca c = new entMarca();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarMarca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMarca", idMarca);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idMarca = Convert.ToInt32(dr["idMarca"]);
                    c.nombreMarca = Convert.ToString(dr["nombreMarca"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return c;
        }
        public Boolean DeshabilitarMarca(entMarca c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("DeshabilitarMarca", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idMarca", c.idMarca);
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
