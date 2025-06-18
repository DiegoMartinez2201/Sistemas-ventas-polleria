using capa;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public class datFormaEnvio
    {
        private static readonly datFormaEnvio instancia = new datFormaEnvio();
        public static datFormaEnvio Instancia => instancia;

        public List<entFormaEnvio> ListarFormasEnvio()
        {
            List<entFormaEnvio> lista = new List<entFormaEnvio>();
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = cConexion.Instancia.Conectar();
                cmd = new SqlCommand("SELECT idFormaEnvio, nombre, estado FROM FormaEnvio WHERE estado = 1", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new entFormaEnvio
                    {
                        idFormaEnvio = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        estado = dr.GetInt32(2)
                    });
                }
            }
            finally
            {
                if (cmd != null) cmd.Connection.Close();
            }
            return lista;
        }
    }
}
