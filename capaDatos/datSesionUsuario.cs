using System;
using System.Data;
using System.Data.SqlClient;
using capaEntidad;

namespace capaDatos
{
    public class datSesionUsuario
    {
        public string CrearSesion(entSesionUsuario sesion)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spCrearSesion", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = sesion.idUsuario;
                comando.Parameters.Add("@token", SqlDbType.VarChar, 255).Value = sesion.token;
                comando.Parameters.Add("@fechaExpiracion", SqlDbType.DateTime).Value = sesion.fechaExpiracion;
                comando.Parameters.Add("@ipAddress", SqlDbType.VarChar, 45).Value = sesion.ipAddress;
                comando.Parameters.Add("@userAgent", SqlDbType.VarChar, 500).Value = sesion.userAgent;
                sqlCon.Open();
                rpta = comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo crear la sesión";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rpta;
        }

        public DataTable ValidarSesion(string token)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spValidarSesion", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@token", SqlDbType.VarChar, 255).Value = token;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                tabla.Load(resultado);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }

        public string CerrarSesion(string token)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spCerrarSesion", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@token", SqlDbType.VarChar, 255).Value = token;
                sqlCon.Open();
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo cerrar la sesión";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rpta;
        }

        public bool VerificarPermiso(int idUsuario, string nombrePermiso)
        {
            SqlDataReader resultado;
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spVerificarPermiso", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                comando.Parameters.Add("@nombrePermiso", SqlDbType.VarChar, 100).Value = nombrePermiso;
                sqlCon.Open();
                resultado = comando.ExecuteReader();
                
                if (resultado.Read())
                {
                    return Convert.ToInt32(resultado["tienePermiso"]) > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
        }
    }
} 