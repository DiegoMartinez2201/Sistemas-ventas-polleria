using capa;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public class datPedidoDelivery
    {
        private static readonly datPedidoDelivery _instancia = new datPedidoDelivery();
        public static datPedidoDelivery Instancia { get { return _instancia; } }
        /*public List<entPedidoDelivery> ListarPedidosPorCliente(int idCliente)
        {
            List<entPedidoDelivery> lista = new List<entPedidoDelivery>();
            using (SqlConnection cn = new SqlConnection("Data Source=DESKTOP-DJ3QFU2\\SQLEXPRESS;Initial Catalog=Polleria;Integrated Security=true;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand("spListarPedidosPorCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new entPedidoDelivery
                    {
                        IdPedido = Convert.ToInt32(dr["IdPedido"]),
                        Direccion = dr["Direccion"].ToString(),
                        Estado = dr["Estado"].ToString()
                    });
                }
            }
            return lista;
        }*/

        /*public void ActualizarEstadoPedido(int idPedido, string nuevoEstado)
            {
                using (SqlConnection cn = new SqlConnection("Data Source=DESKTOP-DJ3QFU2\\SQLEXPRESS;Initial Catalog=Polleria;Integrated Security=true;Encrypt=False;"))
                {
                    SqlCommand cmd = new SqlCommand("spActualizarEstadoPedido", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                    cmd.Parameters.AddWithValue("@NuevoEstado", nuevoEstado);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
         */

    }
}
