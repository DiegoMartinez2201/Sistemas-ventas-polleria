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
    public class DatCliente
    {
        #region singleton
        private static readonly DatCliente UnicaInstancia = new DatCliente();
        public static DatCliente Instancia
        {
            get
            {
                return DatCliente.UnicaInstancia;
            }
        }
        #endregion singleton
        #region metodos
        public List<entCliente> ListarCliente()
        {
            SqlCommand cmd = null;
            List<entCliente> lista = new List<entCliente>();
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entCliente c = new entCliente
                    {
                        idCliente = Convert.ToInt32(dr["idCliente"]),
                        correo = Convert.ToString(dr["correo"]),
                        contraseña = Convert.ToString(dr["contraseña"]),
                        dni = Convert.ToString(dr["dni"]),
                        primernombre = Convert.ToString(dr["primernombre"]),
                        segundonombre = Convert.ToString(dr["segundonombre"]),
                        apellidoPaterno = Convert.ToString(dr["apellidoPaterno"]),
                        apellidoMaterno = Convert.ToString(dr["apellidoMaterno"]),
                        ruc = Convert.ToString(dr["ruc"]),
                        razonSocial = Convert.ToString(dr["razonSocial"]),
                        direccion = Convert.ToString(dr["direccion"]),
                        telefono = Convert.ToString(dr["telefono"]),
                        idTipoCliente = Convert.ToInt32(dr["idTipoCliente"]),
                        estado = Convert.ToInt32(dr["estado"])
                    };
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

        #endregion metodos
    }
}
