using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Sistema_ventas_polleria.Models
{
    public class PedidoEstadoViewModel
    {
        [Required]
        public int IdPedido { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Debes seleccionar un estado")]
        public int IdEstadoNuevo { get; set; }

        // Para poblar el dropdown de estados
        public IEnumerable<SelectListItem> Estados { get; set; }
    }
}
