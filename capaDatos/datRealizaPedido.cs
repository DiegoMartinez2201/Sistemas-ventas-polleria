using capa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaDatos
{
    public class datRealizaPedido
    {
        #region Singleton
        private static readonly datRealizaPedido _instancia = new datRealizaPedido();
        public static datRealizaPedido Instancia => _instancia;
        #endregion

        /// <summary>
        /// Recupera la lista de cabeceras de pedidos con datos maestros:
        /// usuario, estado, forma de envío y método de pago.
        /// </summary>
        private List<entRealizaPedido> ListarCabeceras()
        {
            var lista = new List<entRealizaPedido>();
            using var cn = cConexion.Instancia.Conectar();
            using var cmd = new SqlCommand("spListarRealizaPedido", cn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cn.Open();
            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new entRealizaPedido
                {
                    idPedidoOnline = dr.GetInt32("idPedidoOnline"),
                    idUsuario = dr.GetInt32("idUsuario"),
                    idEstado = dr.GetInt32("idEstado"),
                    idFormaEnvio = dr.GetInt32("idFormaEnvio"),
                    idMetodoPago = dr.GetInt32("idMetodoPago"),
                    fecha = dr.GetDateTime("fecha"),
                    hora = dr.GetTimeSpan(dr.GetOrdinal("hora")),
                    observaciones = dr["observaciones"]?.ToString(),
                    direccion = dr["direccion"]?.ToString(),

                    Usuario = new entUsuario
                    {
                        idUsuario = dr.GetInt32("idUsuario"),
                        correo = dr["correoUsuario"].ToString(),
                        nombreCli = dr["nombreUsuario"].ToString(),
                        apellidoCli = dr["apellidoUsuario"].ToString()
                    },  
                    Estado = new entEstado
                    {
                        idEstado = dr.GetInt32(dr.GetOrdinal("idEstado")),
                        nombreEstado = dr["nombreEstado"].ToString(),
                        estado = dr.GetInt32(dr.GetOrdinal("activoEstado"))
                    },
                    FormaEnvio = new entFormaEnvio
                    {
                        idFormaEnvio = dr.GetInt32("idFormaEnvio"),
                        nombreForma = dr["nombreFormaEnvio"].ToString(),
                        estado = dr.GetInt32(dr.GetOrdinal("activoFormaEnvio"))  // coincide con el SP
                    },
                    MetodoPago = new entMetodoDePago
                    {
                        idMetodoPago = dr.GetInt32("idMetodoPago"),
                        nombreMetodo = dr["nombreMetodoPago"].ToString(),
                        estado = dr.GetInt32(dr.GetOrdinal("activoMetodoPago"))  // coincide con el SP
                    }
                });
            }
            return lista;
        }

        /// <summary>
        /// Recupera todos los pedidos con sus detalles (combos y productos).
        /// </summary>
        public List<entRealizaPedido> ListarTodosConDetalles()
        {
            var pedidos = ListarCabeceras();
            foreach (var p in pedidos)
            {
                p.Combos = datComboProducto.Instancia.ListarPorPedido(p.idPedidoOnline);
                p.Productos = datPedidoProducto.Instancia.ListarPorPedido(p.idPedidoOnline);
            }
            return pedidos;
        }

        /// <summary>
        /// Actualiza el estado de un pedido.
        /// </summary>
        public bool ActualizarEstado(int idPedidoOnline, int nuevoEstado)
        {
            using var cn = cConexion.Instancia.Conectar();
            using var cmd = new SqlCommand("spActualizarEstadoRealizaPedido", cn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@idRealizaPedido", idPedidoOnline);
            cmd.Parameters.AddWithValue("@idEstado", nuevoEstado);
            cn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
