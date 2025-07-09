using System;
using System.Collections.Generic;
using capa;

namespace Sistema_ventas_polleria.Models
{
    public class CartaPedidoViewModel
    {
        public List<entCategoria> Categorias { get; set; }
        public List<entProducto> Productos { get; set; }
        public List<entCombo> Combos { get; set; } // NUEVO: lista de combos
        public Dictionary<int, List<entComboProducto>> ComboProductos { get; set; } // NUEVO: productos por combo
        public List<CarritoItem> Carrito { get; set; } = new List<CarritoItem>();
        public int idFormaEnvio { get; set; }
        public int idMetodoPago { get; set; }
        public string Observaciones { get; set; }
    }

    public class CarritoItem
    {
        public int? idProducto { get; set; } // Puede ser null si es combo
        public int? idCombo { get; set; }    // Puede ser null si es producto
        public string nombreProducto { get; set; }
        public decimal precioUnitario { get; set; }
        public int cantidad { get; set; }
        public bool esCombo { get; set; }   // NUEVO: para distinguir
        public decimal subtotal => cantidad * precioUnitario;
    }
} 