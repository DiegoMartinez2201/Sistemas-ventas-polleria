using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using capa;
using System.Data.SqlClient;
using System.Data;

namespace capaDatos
{
    public class datCategoria
    {
        #region singleton
        private static readonly datCategoria UnicaInstancia = new datCategoria();
        public static datCategoria Instancia
        {
            get
            {
                return datCategoria.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entCategoria> ListarCategoria()
        {
            SqlCommand cmd = null;
            List<entCategoria> lista = new List<entCategoria>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entCategoria c = new entCategoria();
                    c.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    c.nombreCategoria = Convert.ToString(dr["nombreCategoria"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    lista.Add(c);
                }
            }
            catch(SqlException)
            { throw; }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }

        public Boolean InsertarCategoria(entCategoria c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.Parameters.AddWithValue("@nombreCategoria", c.nombreCategoria);
                cmd.Parameters.AddWithValue("@estado", c.estado); 

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    inserta = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return inserta;
        }

        public Boolean EditarCategoria(entCategoria c)
        {
            SqlCommand cmd= null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", c.idCategoria);
                cmd.Parameters.AddWithValue("@nombreCategoria", c.nombreCategoria);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if(i > 0) {edita = true;}
            }
            catch (Exception)
            { throw; }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entCategoria BuscarCategoria(int idCategoria)
        {
            SqlCommand cmd = null;
            entCategoria c = new entCategoria();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    c.nombreCategoria = Convert.ToString(dr["nombreCategoria"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch(Exception) { throw; }
            finally { cmd.Connection.Close(); } 
            return c;
        }
        public Boolean DeshabilitarCategoria(entCategoria c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", c.idCategoria);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    delete = true;
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally { cmd.Connection.Close (); }
            return delete;
        }
        public List<entCategoria> ListarTodasCategorias()
        {
            SqlCommand cmd = null;
            List<entCategoria> lista = new List<entCategoria>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarTodasCategorias", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entCategoria c = new entCategoria();
                    c.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    c.nombreCategoria = Convert.ToString(dr["nombreCategoria"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    lista.Add(c);
                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return lista;
        }
        #endregion metodos
    }
}
