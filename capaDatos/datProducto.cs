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
                    c.idProducto = Convert.ToInt32(dr["idTipoCliente"]);
                    c.nombre = Convert.ToString(dr["nombre"]);
                    c.descripcion = Convert.ToString(dr["descripcion"]);
                    c.precio = Convert.ToDecimal(dr["precio"]);
                    c.stock = Convert.ToInt32(dr["stock"]);
                    lista.Add(c);
                }
            }
            catch (SqlException e)
            { throw e; }
            finally
            {
                cmd.Connection.Close();
            }
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
                cmd.Parameters.AddWithValue("@nombre", c.nombre);
                cmd.Parameters.AddWithValue("@descripcion", c.descripcion);
                cmd.Parameters.AddWithValue("@precio", c.precio);
                cmd.Parameters.AddWithValue("@stock", c.stock);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                { inserta = true; }
            }
            catch (SqlException e) { throw e; }
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
                cmd.Parameters.AddWithValue("@nombre", c.nombre);
                cmd.Parameters.AddWithValue("@descripcion", c.descripcion);
                cmd.Parameters.AddWithValue("@precio", c.precio);
                cmd.Parameters.AddWithValue("@stock", c.stock);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { edita = true; }
            }
            catch (Exception e) { throw e; }
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
                    c.nombre = Convert.ToString(dr["nombre"]);
                    c.descripcion = Convert.ToString(dr["descripcion"]);
                    c.precio = Convert.ToDecimal(dr["precio"]);
                    c.stock = Convert.ToInt32(dr["stock"]);
                }
            }
            catch (Exception e) { throw e; }
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
        #endregion metodos 
    }
}
