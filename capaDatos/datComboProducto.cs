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
    public class datComboProducto
    {
        #region singleton

        private static readonly datComboProducto UnicaInstancia = new datComboProducto();

        public static datComboProducto Instancia

        {
            get

            {
                return datComboProducto.UnicaInstancia;

            }

        }

        #endregion singleton
        #region metodos
        public List<entComboProducto> ListarComboProducto(int idCombo)
        {
            SqlCommand cmd = null;
            List<entComboProducto> lista = new List<entComboProducto>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarComboProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCombo", idCombo);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entComboProducto c = new entComboProducto();
                    c.idComboProducto = Convert.ToInt32(dr["idComboProducto"]);
                    c.idCombo = Convert.ToInt32(dr["idCombo"]);
                    c.idProducto = Convert.ToInt32(dr["idProducto"]);
                    c.cantidad = Convert.ToInt32(dr["cantidad"]);   
                    lista.Add(c);
                }
            }
            catch (SqlException e)
            { throw e; }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return lista;
        }

        public Boolean InsertarComboProducto(entComboProducto c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarComboProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idCombo", c.idCombo);
                cmd.Parameters.AddWithValue("@idProducto", c.idProducto);
                cmd.Parameters.AddWithValue("@cantidad", c.cantidad);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                { inserta = true; }
            }
            catch (SqlException e) { throw e; }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return inserta;
        }
        public Boolean EditaComboProducto(entComboProducto c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaComboProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros que espera el procedimiento almacenado
                cmd.Parameters.AddWithValue("@idComboProducto", c.idComboProducto);
                cmd.Parameters.AddWithValue("@nuevoIdProducto", c.idProducto); // usa el nuevo nombre
                cmd.Parameters.AddWithValue("@nuevaCantidad", c.cantidad);     // usa el nuevo nombre

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { edita = true; }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return edita;
        }

        public entComboProducto BuscarComboProducto(int idComboProducto)
        {
            SqlCommand cmd = null;
            entComboProducto c = new entComboProducto();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarComboProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idComboProducto", idComboProducto);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idComboProducto = Convert.ToInt32(dr["idComboProducto"]);
                    c.idCombo = Convert.ToInt32(dr["idCombo"]);
                    c.idProducto = Convert.ToInt32(dr["idProducto"]);
                    c.cantidad = Convert.ToInt32(dr["cantidad"]);
                }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return c;
        }

        public Boolean EliminarComboProducto(int idComboProducto)
        {
            SqlCommand cmd = null;
            Boolean elimina = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarComboProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idComboProducto", idComboProducto);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { elimina = true; }
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return elimina;
        }

        /// <summary>
        /// Lista los combos asociados a un pedido online.
        /// </summary>
        public List<entPedidoCombo> ListarPorPedido(int idPedidoOnline)
        {
            var lista = new List<entPedidoCombo>();
            using var cn = cConexion.Instancia.Conectar();
            using var cmd = new SqlCommand("spListarPedidoComboPorPedido", cn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@idPedidoOnline", idPedidoOnline);
            cn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new entPedidoCombo
                {
                    idPedidoCombo = dr.GetInt32(dr.GetOrdinal("idPedidoCombo")),
                    idPedidoOnline = dr.GetInt32(dr.GetOrdinal("idPedidoOnline")),
                    idCombo = dr.GetInt32(dr.GetOrdinal("idCombo")),
                    cantidad = dr.GetInt32(dr.GetOrdinal("cantidad"))
                });
            }
            return lista;
        }
        #endregion metodos
    }
}
