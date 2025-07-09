using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using capaDatos;
using System.Data.SqlClient;
using System.Data;
using capa;
using capaLogica;

namespace Sistema_ventas_polleria.Controllers
{
    public class CuentaController : Controller
    {
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(
            string tipoCliente, string dni, string nombreCli, string apellidoCli,
            string ruc, string razonSocial, string correo, string contraseña,
            string direccion, string telefono)
        {
            var usuario = new entUsuario
            {
                dni = tipoCliente == "persona" ? dni : null,
                nombreCli = tipoCliente == "persona" ? nombreCli : null,
                apellidoCli = tipoCliente == "persona" ? apellidoCli : null,
                ruc = tipoCliente == "empresa" ? ruc : null,
                razonSocial = tipoCliente == "empresa" ? razonSocial : null,
                correo = correo,
                contraseña = contraseña, // ¡En producción, hashea la contraseña!
                direccion = direccion,
                telefono = telefono,
                idRol = 3, // Siempre cliente
                estado = 1
            };

            var resultado = logUsuario.Instancia.InsertarUsuario(usuario);

            if (resultado)
                return RedirectToAction("Login", "Seguridad");
            else
            {
                TempData["Error"] = "No se pudo registrar el usuario.";
                return View();
            }
        }

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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Seguridad");
        }
    }
}
