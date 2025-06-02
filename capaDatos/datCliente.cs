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
    public class datCliente
    {
       /* #region singleton
        private static readonly datCliente UnicaInstancia = new datCliente();
        public static datCliente Instancia
        {
            get
            {
                return datCliente.UnicaInstancia;
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
                    entCliente c = new entCliente();
                    c.idCliente = Convert.ToInt32(dr["idCliente"]);
                    c.correo = Convert.ToString(dr["correo"]);
                    c.contraseña = Convert.ToString(dr["contraseña"]);
                    c.dni = Convert.ToString(dr["dni"]);
                    c.primernombre = Convert.ToString(dr["primernombre"]);
                    c.segundonombre = Convert.ToString(dr["segundonombre"]);
                    c.apellidoPaterno = Convert.ToString(dr["apellidoPaterno"]);
                    c.apellidoMaterno = Convert.ToString(dr["apellidoMaterno"]);
                    c.ruc = Convert.ToString(dr["ruc"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.direccion = Convert.ToString(dr["direccion"]);
                    c.telefono = Convert.ToString(dr["telefono"]);
                    c.idTipoCliente = Convert.ToInt32(dr["idTipoCliente"]);
                    c.estado = Convert.ToInt32(dr["estado"]);
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

        #endregion metodos*/
    }
}
