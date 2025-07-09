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
    public class datPedidoProducto
    {
        #region singleton

        private static readonly datPedidoProducto UnicaInstancia = new datPedidoProducto();

        public static datPedidoProducto Instancia

        {
            get

            {
                return datPedidoProducto.UnicaInstancia;

            }

        }

        #endregion singleton
        #region metodos

        public List<entPedidoProducto> ListarPedidoProducto()

        {
            SqlCommand cmd = null;
            List<entPedidoProducto> lista = new List<entPedidoProducto>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entPedidoProducto c = new entPedidoProducto();
                    c.idPedidoProducto = Convert.ToInt32(dr["idPedidoProducto"]);
                    
                    lista.Add(c);
                }
            }

            catch (SqlException e)
            { throw e; }
            finally
            { cmd.Connection.Close(); }
            return lista;
        }
        public Boolean InsertarProducto(entProducto c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", c.idCategoria);
                cmd.Parameters.AddWithValue("@idMarca", c.idMarca);
                cmd.Parameters.AddWithValue("@idTamaño", c.idTamaño);
                cmd.Parameters.AddWithValue("@nombreProducto", c.nombreProducto);
                cmd.Parameters.AddWithValue("@precioVenta", c.precioVenta);
                cmd.Parameters.AddWithValue("@stock", c.stock);
                cmd.Parameters.AddWithValue("@descripcionP", c.descripcionP);
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
        public Boolean EditaProducto(entProducto c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", c.idCategoria);
                cmd.Parameters.AddWithValue("@idMarca", c.idMarca);
                cmd.Parameters.AddWithValue("@idTamaño", c.idTamaño);
                cmd.Parameters.AddWithValue("@nombreProducto", c.nombreProducto);
                cmd.Parameters.AddWithValue("@precioVenta", c.precioVenta);
                cmd.Parameters.AddWithValue("@stock", c.stock);
                cmd.Parameters.AddWithValue("@descripcionP", c.descripcionP);
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
        public entProducto BuscarProducto(int idProducto)
        {
            SqlCommand cmd = null;
            entProducto c = new entProducto();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idProducto = Convert.ToInt32(dr["idProducto"]);
                    c.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    c.idMarca = Convert.ToInt32(dr["idMarca"]);
                    c.idTamaño = Convert.ToInt32(dr["idTamaño"]);
                    c.nombreProducto = Convert.ToString(dr["nombreProducto"]);
                    c.precioVenta = Convert.ToDecimal(dr["precioVenta"]);
                    c.stock = Convert.ToInt32(dr["stock"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    c.descripcionP = Convert.ToString(dr["descripcionP"]);
                }
            }
            catch (Exception e)
            { throw e; }
            finally { cmd.Connection.Close(); }
            return c;
        }
        public Boolean DeshabilitarProducto(entProducto c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProducto", c.idProducto);
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
        public int InsertarPedidoOnline(entRealizaPedido pedido)
        {
            SqlCommand cmd = null;
            int idPedidoOnline = 0;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarPedidoOnline", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", pedido.idUsuario);
                cmd.Parameters.AddWithValue("@idEstado", pedido.idEstado);
                cmd.Parameters.AddWithValue("@idFormaEnvio", pedido.idFormaEnvio);
                cmd.Parameters.AddWithValue("@idMetodoPago", pedido.idMetodoPago);
                cmd.Parameters.AddWithValue("@fecha", pedido.fecha);
                cmd.Parameters.AddWithValue("@hora", pedido.hora);
                cmd.Parameters.AddWithValue("@observaciones", (object)pedido.observaciones ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@direccion", (object)pedido.direccion ?? DBNull.Value);

                SqlParameter outputId = new SqlParameter("@idPedidoOnline", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                cn.Open();
                cmd.ExecuteNonQuery();
                idPedidoOnline = Convert.ToInt32(outputId.Value);
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return idPedidoOnline;
        }

        public bool InsertarPedidoProducto(entPedidoProducto detalle)
        {
            SqlCommand cmd = null;
            bool inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarPedidoProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPedidoOnline", detalle.idPedidoOnline);
                cmd.Parameters.AddWithValue("@idProducto", detalle.idProducto);
                cmd.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", detalle.precioUnitario);
                cmd.Parameters.AddWithValue("@subtotal", detalle.subtotal);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                inserta = i > 0;
            }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return inserta;
        }
        public bool InsertarPedidoCombo(entPedidoCombo combo)
        {
            SqlCommand cmd = null;
            bool inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarPedidoCombo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPedidoOnline", combo.idPedidoOnline);
                cmd.Parameters.AddWithValue("@idCombo", combo.idCombo);
                cmd.Parameters.AddWithValue("@cantidad", combo.cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", combo.precioUnitario);
                cmd.Parameters.AddWithValue("@subtotal", combo.subtotal);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) inserta = true;
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (cmd != null && cmd.Connection != null)
                    cmd.Connection.Close();
            }
            return inserta;
        }
        #endregion metodos


    }
}
