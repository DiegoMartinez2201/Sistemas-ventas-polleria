using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using capaEntidad;

namespace capaDatos
{
    public class datTipoCliente
    {
        /*#region singleton
        private static readonly datTipoCliente UnicaInstancia = new datTipoCliente();
        public static datTipoCliente Instancia
        {
            get
            {
                return datTipoCliente.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entTipoCliente> ListarTipoCliente()
        {
            SqlCommand cmd = null;
            List<entTipoCliente> lista = new List<entTipoCliente>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarTipoCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entTipoCliente c = new entTipoCliente();
                    c.idTipoCliente = Convert.ToInt32(dr["idTipoCliente"]);
                    c.nombre = Convert.ToString(dr["nombre"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                    lista.Add(c);
                }
            }
            catch(SqlException e)
            { throw e; }
            finally
            {
                cmd.Connection.Close();
            }
            return lista;
        }
        
        public Boolean InsertarTipoCliente(entTipoCliente c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarTipoCliente", cn);
                cmd.Parameters.AddWithValue("@nombre", c.nombre);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) 
                {inserta = true;}
            }
            catch(SqlException e) { throw e; }
            finally {  cmd.Connection.Close(); }
            return inserta;
        }
        public Boolean EditaTipoCliente(entTipoCliente c)
        {
            SqlCommand cmd= null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaTipoCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTipoCliente", c.idTipoCliente);
                cmd.Parameters.AddWithValue("@nombre", c.nombre);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if(i > 0) {edita = true;}
            }
            catch (Exception e){ throw e; }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entTipoCliente BuscarTipoCliente(int idTipoCliente)
        {
            SqlCommand cmd = null;
            entTipoCliente c = new entTipoCliente();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarTipoCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTipoCliente", idTipoCliente);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idTipoCliente = Convert.ToInt32(dr["idTipoCliente"]);
                    c.nombre = Convert.ToString(dr["nombre"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch(Exception e) { throw e; }
            finally { cmd.Connection.Close(); } 
            return c;
        }
        public Boolean DeshabilitarTipoCliente(entTipoCliente c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarTipoCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTipoCliente", c.idTipoCliente);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    delete = true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close (); }
            return delete;
        }
        #endregion metodos*/
    }
}
