using System;
using System.Data;
using System.Data.SqlClient;
using capaEntidad;

namespace capaDatos
{
    public class datPermiso
    {
        public DataTable ListarPermisos()
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spListarPermisos", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
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

        public DataTable BuscarPermiso(int idPermiso)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spBuscarPermiso", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idPermiso", SqlDbType.Int).Value = idPermiso;
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

        public string InsertarPermiso(entPermiso permiso)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spInsertarPermiso", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@nombrePermiso", SqlDbType.VarChar, 100).Value = permiso.nombrePermiso;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar, 255).Value = permiso.descripcion;
                sqlCon.Open();
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo insertar el permiso";
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

        public string EditarPermiso(entPermiso permiso)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spEditarPermiso", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idPermiso", SqlDbType.Int).Value = permiso.idPermiso;
                comando.Parameters.Add("@nombrePermiso", SqlDbType.VarChar, 100).Value = permiso.nombrePermiso;
                comando.Parameters.Add("@descripcion", SqlDbType.VarChar, 255).Value = permiso.descripcion;
                sqlCon.Open();
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo editar el permiso";
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

        public string DeshabilitarPermiso(int idPermiso)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spDeshabilitarPermiso", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idPermiso", SqlDbType.Int).Value = idPermiso;
                sqlCon.Open();
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo deshabilitar el permiso";
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
    }
} 