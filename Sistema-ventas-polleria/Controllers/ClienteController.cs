using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_ventas_polleria.Models;
using capa;
using capaLogica;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sistema_ventas_polleria.Controllers
{
    
    public class ClienteController : Controller
    {
        public IActionResult Clientelog()
        {
            /*if (User.FindFirst("idTipoCliente")?.Value != "2")
                return Unauthorized();*/

            return View();
        }
        [HttpGet]
        public IActionResult Carta()
        {
            var combos = logCombo.Instancia.ListarCombo();
            var comboProductos = new Dictionary<int, List<entComboProducto>>();
            foreach (var combo in combos)
            {
                comboProductos[combo.idCombo] = capaLogica.logComboProducto.Instancia.ListarComboProducto(combo.idCombo);
            }
            var model = new CartaPedidoViewModel
            {
                Categorias = logCategoria.Instancia.ListarCategoria(),
                Productos = logProducto.Instancia.ListarProducto(),
                Combos = combos,
                ComboProductos = comboProductos,
                Carrito = new List<CarritoItem>()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult RealizarPedido(string direccion, string telefono, string referencia, string enlaceGoogleMaps, string CarritoJson, int formaEnvio, int metodoPago)
        {
            try
            {
                if (string.IsNullOrEmpty(CarritoJson))
                    return Content("No se recibió el carrito.");
                var carrito = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DetalleProducto>>(CarritoJson);
                if (carrito == null || carrito.Count == 0)
                    return Content("El carrito está vacío.");
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
                if (idUsuario == null)
                    return Content("Error: Sesión expirada o usuario no logueado.");
                var pedido = new entRealizaPedido
                {
                    idUsuario = idUsuario.Value,
                    idEstado = 1, // Preparando
                    idFormaEnvio = formaEnvio,
                    idMetodoPago = metodoPago, // <-- Ahora dinámico
                    fecha = DateTime.Now.Date,
                    hora = DateTime.Now,
                    observaciones = (referencia ?? "") + ((enlaceGoogleMaps != null && enlaceGoogleMaps != "") ? (" | Google Maps: " + enlaceGoogleMaps) : ""),
                    direccion = direccion
                };
                int idPedidoOnline = logPedidoProducto.Instancia.InsertarPedidoOnline(pedido);
                foreach (var item in carrito)
                {
                    if (item.idProducto != null && !item.esCombo)
                    {
                        var detalle = new capa.entPedidoProducto
                        {
                            idPedidoOnline = idPedidoOnline,
                            idProducto = item.idProducto.Value,
                            cantidad = item.cantidad,
                            precioUnitario = item.precioUnitario,
                            subtotal = item.precioUnitario * item.cantidad
                        };
                        logPedidoProducto.Instancia.InsertarPedidoProducto(detalle);
                    }
                    else if (item.idCombo != null && item.esCombo)
                    {
                        var detalleCombo = new capa.entPedidoCombo
                        {
                            idPedidoOnline = idPedidoOnline,
                            idCombo = item.idCombo.Value,
                            cantidad = item.cantidad,
                            precioUnitario = item.precioUnitario,
                            subtotal = item.precioUnitario * item.cantidad
                        };
                        logPedidoProducto.Instancia.InsertarPedidoCombo(detalleCombo);
                    }
                }
                // Limpiar carrito del localStorage (puedes hacerlo con JS en la vista de confirmación)
                return RedirectToAction("ConfirmacionPedido");
            }
            catch (Exception ex)
            {
                return Content("Error al registrar el pedido: " + ex.Message);
            }
        }

        public IActionResult ConfirmacionPedido()
        {
            return View();
        }
        public IActionResult Delivery()
        {
            return View();
        }
    }

    public class PedidoViewModel
    {
        public int idFormaEnvio { get; set; }
        public int idMetodoPago { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string observaciones { get; set; }
        public List<DetalleProducto> productos { get; set; }
    }
    public class DetalleProducto
    {
        public int? idProducto { get; set; } // nullable para combos
        public int? idCombo { get; set; }    // nullable para productos
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }
        public bool esCombo { get; set; }   // para distinguir combos
    }
}
