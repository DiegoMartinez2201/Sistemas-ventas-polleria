using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using capaDatos;
using System.Data.SqlClient;
using System.Data;


namespace Sistema_ventas_polleria.Controllers
{
    public class CuentaController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {
            using (SqlConnection conexion = cConexion.Instancia.Conectar())
            {
                string query = "SELECT * FROM Cliente WHERE correo = @correo AND contraseña = @contraseña";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@contraseña", contraseña);

                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int tipo = Convert.ToInt32(reader["idTipoCliente"]);
                    string nombre = reader["correo"].ToString();

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, nombre),
                    new Claim("idTipoCliente", tipo.ToString())
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    if (tipo == 1)
                    {
                        return Json(new { success = true, redirect = "/Administrador/Dashboard" });
                    }
                    else if (tipo == 2)
                    {
                        return Json(new { success = true, redirect = "/Cliente/Clientelog" });
                    }
                    else
                    {
                        return Json(new { success = false, mensaje = "Tipo de usuario no válido" });
                    }
                }
                else
                {
                    return Json(new { success = false, mensaje = "Usuario o clave incorrectos" });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Json(new { success = true });
        }
    }
}
