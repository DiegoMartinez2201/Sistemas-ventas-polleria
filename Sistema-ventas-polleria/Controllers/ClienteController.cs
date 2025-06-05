using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sistema_ventas_polleria.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
       public IActionResult Clientelog()
        {
            if (User.FindFirst("idTipoCliente")?.Value != "2")
                return Unauthorized();

            return View();
        }
    }
}
