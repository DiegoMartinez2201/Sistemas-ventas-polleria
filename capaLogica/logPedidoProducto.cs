using capa;
using capaDatos;
using System;
using System.Collections.Generic;

namespace capaLogica
{
    public class logPedidoProducto
    {
        #region singleton
        private static readonly logPedidoProducto UnicaInstancia = new logPedidoProducto();
        public static logPedidoProducto Instancia
        {
            get { return logPedidoProducto.UnicaInstancia; }
        }
        #endregion singleton

        public int InsertarPedidoOnline(entRealizaPedido pedido)
        {
            return datPedidoProducto.Instancia.InsertarPedidoOnline(pedido);
        }

        public bool InsertarPedidoProducto(entPedidoProducto detalle)
        {
            return datPedidoProducto.Instancia.InsertarPedidoProducto(detalle);
        }

        public bool InsertarPedidoCombo(entPedidoCombo combo)
        {
            try
            {
                return datPedidoProducto.Instancia.InsertarPedidoCombo(combo);
            }
            catch (Exception) { throw; }
        }
    }
} 