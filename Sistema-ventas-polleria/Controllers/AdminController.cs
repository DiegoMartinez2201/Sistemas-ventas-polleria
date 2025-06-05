using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    [Authorize]
    public class AdministradorController : Controller
    {
        public IActionResult Dashboard()
        {
            if (User.FindFirst("idTipoCliente")?.Value != "1")
                return Unauthorized();

            return View();
        }
    }
}
