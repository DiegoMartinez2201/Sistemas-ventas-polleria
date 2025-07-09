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
    public class datProducto
    {
        #region singleton
        private static readonly datProducto UnicaInstancia = new datProducto();
        public static datProducto Instancia
        {
            get
            {
                return datProducto.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos

        public List<entProducto> ListarProducto()

        {
            SqlCommand cmd = null;
            List<entProducto> lista = new List<entProducto>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProducto c = new entProducto();
                    c.idProducto = Convert.ToInt32(dr["idProducto"]);
                    c.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    c.idMarca = Convert.ToInt32(dr["idMarca"]);
                    c.idTamaño = Convert.ToInt32(dr["idTamaño"]);
                    c.nombreProducto = Convert.ToString(dr["nombreProducto"]);
                    c.precioVenta = Convert.ToDecimal(dr["precioVenta"]);
                    c.stock = Convert.ToInt32(dr["stock"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    c.descripcionP = dr["descripcionP"] == DBNull.Value ? null : dr["descripcionP"].ToString();
                    c.imagen = dr["imagen"] == DBNull.Value ? null : dr["imagen"].ToString();
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
                cmd.Parameters.AddWithValue("@imagen", c.imagen ?? (object)DBNull.Value);
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
                cmd.Parameters.AddWithValue("@idProducto", c.idProducto);
                cmd.Parameters.AddWithValue("@idCategoria", c.idCategoria);
                cmd.Parameters.AddWithValue("@idMarca", c.idMarca);
                cmd.Parameters.AddWithValue("@idTamaño", c.idTamaño);
                cmd.Parameters.AddWithValue("@nombreProducto", c.nombreProducto);
                cmd.Parameters.AddWithValue("@precioVenta", c.precioVenta);
                cmd.Parameters.AddWithValue("@stock", c.stock);
                cmd.Parameters.AddWithValue("@descripcionP", c.descripcionP);
                cmd.Parameters.AddWithValue("@imagen", c.imagen ?? (object)DBNull.Value);
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
        public List<entProducto> ListarProductoPorCategoria(int idCategoria)
        {
            SqlCommand cmd = null;
            List<entProducto> lista = new List<entProducto>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarProductoPorCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entProducto c = new entProducto();
                    c.idProducto = Convert.ToInt32(dr["idProducto"]);
                    c.idCategoria = Convert.ToInt32(dr["idCategoria"]);
                    c.nombreProducto = dr["nombreProducto"].ToString();
                    c.precioVenta = Convert.ToDecimal(dr["precioVenta"]);
                    c.descripcionP = dr["descripcionP"].ToString();
                    c.imagen = dr["imagen"] == DBNull.Value ? null : dr["imagen"].ToString();
                    lista.Add(c);
                }
            }
            finally { cmd.Connection.Close(); }
            return lista;
        }
        #endregion metodos
    }
}
