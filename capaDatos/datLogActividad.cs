using System;
using System.Data;
using System.Data.SqlClient;
using capa;

namespace capaDatos
{
    public class datLogActividad
    {
        public string RegistrarActividad(entLogActividad log)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spRegistrarActividad", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = log.idUsuario;
                comando.Parameters.Add("@accion", SqlDbType.VarChar, 100).Value = log.accion;
                comando.Parameters.Add("@tabla", SqlDbType.VarChar, 100).Value = log.tabla;
                comando.Parameters.Add("@idRegistro", SqlDbType.Int).Value = log.idRegistro;
                comando.Parameters.Add("@datosAnteriores", SqlDbType.Text).Value = log.datosAnteriores;
                comando.Parameters.Add("@datosNuevos", SqlDbType.Text).Value = log.datosNuevos;
                comando.Parameters.Add("@ipAddress", SqlDbType.VarChar, 45).Value = log.ipAddress;
                sqlCon.Open();
                rpta = comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo registrar la actividad";
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

        public DataTable ListarLogs(DateTime fechaInicio, DateTime fechaFin)
        {
            SqlDataReader resultado;
            DataTable tabla = new DataTable();
            SqlConnection sqlCon = new SqlConnection();

            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spListarLogs", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                comando.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = fechaFin;
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

        public string LimpiarLogsAntiguos(int diasRetener = 90)
        {
            string rpta = "";
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon = cConexion.Instancia.Conectar();
                SqlCommand comando = new SqlCommand("spLimpiarLogsAntiguos", sqlCon);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@diasRetener", SqlDbType.Int).Value = diasRetener;
                sqlCon.Open();
                comando.ExecuteNonQuery();
                rpta = "OK";
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