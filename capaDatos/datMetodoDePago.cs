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
        public class datMetodoDePago
        {
            #region singleton
            private static readonly datMetodoDePago UnicaInstancia = new datMetodoDePago();
            public static datMetodoDePago Instancia
            {
                get
                {
                    return datMetodoDePago.UnicaInstancia;
                }
            }
            #endregion singleton
            #region metodos
            public List<entMetodoDePago> ListarMetodoDePago()
            {
                SqlCommand cmd = null;
                List<entMetodoDePago> lista = new List<entMetodoDePago>();
                try
                {
                    SqlConnection cn = cConexion.Instancia.Conectar();
                    cmd = new SqlCommand("spListarMetodoDePago", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        entMetodoDePago c = new entMetodoDePago();
                        c.idMetodoPago = Convert.ToInt32(dr["idMetodoPago"]);
                        c.nombreMetodo = Convert.ToString(dr["nombreMetodo"]);
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

            public Boolean InsertarMetodoDePago(entMetodoDePago c)
            {
                SqlCommand cmd = null;
                Boolean inserta = false;
                try
                {
                    SqlConnection cn = cConexion.Instancia.Conectar();
                    cmd = new SqlCommand("spInsertarMetodoDePago", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreMetodo", c.nombreMetodo);
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
            public Boolean EditarMetodoDePago(entMetodoDePago c)
            {
                SqlCommand cmd = null;
                Boolean edita = false;
                try
                {
                    SqlConnection cn = cConexion.Instancia.Conectar();
                    cmd = new SqlCommand("spEditarMetodoDePago", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idMetodoPago", c.idMetodoPago);
                    cmd.Parameters.AddWithValue("@nombreMetodo", c.nombreMetodo);
                    cn.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0) { edita = true; }
                }
                catch (Exception) { throw; }
                finally { cmd.Connection.Close(); }
                return edita;
            }
            public entMetodoDePago BuscarMetodoDePago(int idMetodoPago)
            {
                SqlCommand cmd = null;
                entMetodoDePago c = new entMetodoDePago();
                try
                {
                    SqlConnection cn = cConexion.Instancia.Conectar();
                    cmd = new SqlCommand("spBuscarMetodoDePago", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idMetodoPago", idMetodoPago);
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        c.idMetodoPago = Convert.ToInt32(dr["idMetodoPago"]);
                        c.nombreMetodo = Convert.ToString(dr["nombreMetodo"]);
                        c.estado = Convert.ToInt32(dr["estado"]);
                    }
                }
                catch (Exception) { throw; }
                finally { cmd.Connection.Close(); }
                return c;
            }
            public Boolean DeshabilitarMetodoDePago(entMetodoDePago c)
            {
                SqlCommand cmd = null;
                Boolean delete = false;
                try
                {
                    SqlConnection cn = cConexion.Instancia.Conectar();
                    cmd = new SqlCommand("spDeshabilitarMetodoDePago", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idMetodoPago", c.idMetodoPago);
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
            public List<entMetodoDePago> ListarTodosMetodosDePago()
            {
                SqlCommand cmd = null;
                List<entMetodoDePago> lista = new List<entMetodoDePago>();
                try
                {
                    SqlConnection cn = cConexion.Instancia.Conectar();
                    cmd = new SqlCommand("spListarTodosMetodosDePago", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        entMetodoDePago c = new entMetodoDePago();
                        c.idMetodoPago = Convert.ToInt32(dr["idMetodoPago"]);
                        c.nombreMetodo = Convert.ToString(dr["nombreMetodo"]);
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
