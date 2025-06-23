using capa;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public class datTamaño
    {
        #region singleton
        private static readonly datTamaño UnicaInstancia = new datTamaño();
        public static datTamaño Instancia
        {
            get
            {
                return datTamaño.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entTamaño> ListarTamaño()
        {
            SqlCommand cmd = null;
            List<entTamaño> lista = new List<entTamaño>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarTamaño", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entTamaño c = new entTamaño();
                    c.idTamaño = Convert.ToInt32(dr["idTamaño"]);
                    c.nombre = Convert.ToString(dr["nombre"]);
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

        public Boolean InsertarTamaño(entTamaño c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarTamaño", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", c.nombre);
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
        public Boolean EditarTamaño(entTamaño c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarTamaño", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTamaño", c.idTamaño);
                cmd.Parameters.AddWithValue("@nombre", c.nombre);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { edita = true; }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entTamaño BuscarTamaño(int idTamaño)
        {
            SqlCommand cmd = null;
            entTamaño c = new entTamaño();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarTamaño", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTamaño", idTamaño);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idTamaño = Convert.ToInt32(dr["idTamaño"]);
                    c.nombre = Convert.ToString(dr["nombre"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return c;
        }
        public Boolean DeshabilitarTamaño(entTamaño c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarTamaño", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTamaño", c.idTamaño);
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