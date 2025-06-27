using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using capa;

namespace capaDatos
{
    public class datUsuario
    {
        #region singleton
        private static readonly datUsuario UnicaInstancia = new datUsuario();
        public static datUsuario Instancia
        {
            get
            {
                return datUsuario.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entUsuario> ListarUsuario()
        {
            SqlCommand cmd = null;
            List<entUsuario> lista = new List<entUsuario>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entUsuario c = new entUsuario();
                    c.idUsuario = Convert.ToInt32(dr["idUsuario"]);
                    c.dni = Convert.ToString(dr["dni"]);
                    c.nombreCli = Convert.ToString(dr["nombreCli"]);
                    c.apellidoCli = Convert.ToString(dr["apellidoCli"]);
                    c.correo = Convert.ToString(dr["correo"]);
                    c.contraseña = Convert.ToString(dr["contraseña"]);
                    c.ruc = Convert.ToString(dr["ruc"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.direccion = Convert.ToString(dr["direccion"]);
                    c.telefono = Convert.ToString(dr["telefono"]);
                    c.idRol = Convert.ToInt32(dr["idRol"]);
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
        
        public Boolean InsertarUsuario(entUsuario c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd = new SqlCommand("spInsertarUsuario", cn);
                cmd.Parameters.AddWithValue("@dni", c.dni);
                cmd.Parameters.AddWithValue("@nombreCli", c.nombreCli);
                cmd.Parameters.AddWithValue("@apellidoCli", c.apellidoCli);
                cmd.Parameters.AddWithValue("@correo", c.correo);
                cmd.Parameters.AddWithValue("@contraseña", c.contraseña);
                cmd.Parameters.AddWithValue("@ruc", c.ruc);
                cmd.Parameters.AddWithValue("@razonSocial", c.razonSocial);
                cmd.Parameters.AddWithValue("@direccion", c.direccion);
                cmd.Parameters.AddWithValue("@telefono", c.telefono);
                cmd.Parameters.AddWithValue("@idRol", c.idRol);
                
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) 
                {inserta = true;}
            }
            catch(SqlException e) { throw e; }
            finally {  cmd.Connection.Close(); }
            return inserta;
        }
        public Boolean EditaUsuario(entUsuario c)
        {
            SqlCommand cmd= null;
            Boolean edita = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditaUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", c.idUsuario);
                cmd.Parameters.AddWithValue("@dni", c.dni);
                cmd.Parameters.AddWithValue("@nombreCli", c.nombreCli);
                cmd.Parameters.AddWithValue("@apellidoCli", c.apellidoCli);
                cmd.Parameters.AddWithValue("@correo", c.correo);
                cmd.Parameters.AddWithValue("@contraseña", c.contraseña);
                cmd.Parameters.AddWithValue("@ruc", c.ruc);
                cmd.Parameters.AddWithValue("@razonSocial", c.razonSocial);
                cmd.Parameters.AddWithValue("@direccion", c.direccion);
                cmd.Parameters.AddWithValue("@telefono", c.telefono);
                
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if(i > 0) {edita = true;}
            }
            catch (Exception e){ throw e; }
            finally { cmd.Connection.Close(); }
            return edita;
        }
        public entUsuario BuscarUsuario(int idUsuario)
        {
            SqlCommand cmd = null;
            entUsuario c = new entUsuario();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idUsuario = Convert.ToInt32(dr["idUsuario"]);
                    c.dni = Convert.ToString(dr["dni"]);
                    c.nombreCli = Convert.ToString(dr["nombreCli"]);
                    c.apellidoCli = Convert.ToString(dr["apellidoCli"]);
                    c.correo = Convert.ToString(dr["correo"]);
                    c.contraseña = Convert.ToString(dr["contraseña"]);
                    c.ruc = Convert.ToString(dr["ruc"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.direccion = Convert.ToString(dr["direccion"]);
                    c.telefono = Convert.ToString(dr["telefono"]);
                    c.idRol = Convert.ToInt32(dr["idRol"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
                }
            }
            catch(Exception e) { throw e; }
            finally { cmd.Connection.Close(); } 
            return c;
        }
        public Boolean DeshabilitarUsuario(entUsuario c)
        {
            SqlCommand cmd = null;
            Boolean delete = false;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", c.idUsuario);
                
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
        #endregion metodos
    }


}
