using Microsoft.AspNetCore.Mvc;
using tecno.Models;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using tecno.Servicio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace tecno.Controllers
{
    public class RegistrarController : Controller
    {
        private IServicio_API _servicioApi;

        public RegistrarController(IServicio_API servicioApi)
        {
            _servicioApi = servicioApi;
        }
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        void connectionString()
        {
            con.ConnectionString = "Data source=DESKTOP-7OHKGLF\\SQLEXPRESS; Initial catalog= 'db_tecnoferre'; integrated security= true; TrustServerCertificate=Yes;";
        }
        public async Task<IActionResult> Login()
        {
            if (HttpContext.Request.Path == "/Registrar/Login")
            {
                ViewData["MensajeError"] = "Inicia sesión primero";
                return View();
            }
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(ViewUsuario usuario)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from usuario where correo='" + usuario.correo + "' and contrasena='" + usuario.contrasena + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, reader["nombre"].ToString()),
                    new Claim("Correo", reader["correo"].ToString()),
                    new Claim("Cedula", reader["cedula"].ToString())
                };
                if ((int)reader["rol"] == 1)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    con.Close();
                    return RedirectToAction("Admin", "Producto");
                }
                else if ((int)reader["rol"] == 2)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Cliente"));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    con.Close();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    con.Close();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewData["MensajeError"] = "No se encontró el usuario o la contraseña es incorrecta";
                con.Close();
                return View();
            }

        }

        public async Task<IActionResult> Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(ViewUsuario usuario)
        {
            Regex regex = new Regex(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$");
            Match match = regex.Match(usuario.contrasena);

            if (usuario.contrasena.Length < 7)
            {
                ViewData["MensajeError"] = "La contraseña debe de ser mayor a 7 caracteres";
                return View();

            }
            else if (!match.Success)
            {
                ViewData["MensajeError"] = "La contraseña debe de contener mayúsculas, números y letras";
                return View();
            }
            else if(usuario.contrasena == usuario.confirmarContrasena)
            {
                bool existeUs = existeCliente(usuario);
                bool existeCo = existeCorreo(usuario);
                if (!existeUs && !existeCo)
                {
                    connectionString();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = string.Format("insert into usuario values({0}, '{1}', '{2}', '{3}', '{4}', {5})", usuario.cedula, usuario.nombre, usuario.correo, usuario.contrasena, usuario.direccion, 2);
                    int usuarioNuevo = (int)cmd.ExecuteNonQuery();
                    if (usuarioNuevo > 0)
                    {
                        con.Close();
                        TempData["MensajeExito"] = "Se ha registrado correctamente";
                        return RedirectToAction("Login", "Registrar", new { id = "IniciarSesion" });
                    }
                    else
                    {
                        ViewData["MensajeError"] = "Hubo un error";
                        reader.Close();
                        con.Close();
                        return View();
                    }
                }
                else
                {
                    ViewData["MensajeError"] = "Ingrese otra identificación o correo";
                    reader.Close();
                    con.Close();
                    return View();
                }

            }
            else
            {
                ViewData["MensajeError"] = "Las contraseñas no son iguales";
                return View();
            }
        }

        public async Task<IActionResult> Mensajero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Mensajero(ViewUsuario usuario)
        {
            if (usuario.contrasena == usuario.confirmarContrasena)
            {
                bool existeUs = existeCliente(usuario);
                bool existeCo = existeCorreo(usuario);
                Debug.WriteLine(existeCliente(usuario));
                if (!existeUs && !existeCo)
                {
                    connectionString();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = string.Format("insert into usuario  (cedula, nombre, correo, contrasena, direccion, rol)  values({0}, '{1}', '{2}', '{3}',' ', '{4}')", usuario.cedula, usuario.nombre, usuario.correo, usuario.contrasena, 3);
                    int usuarioNuevo = (int)cmd.ExecuteNonQuery();
                    if (usuarioNuevo > 0)
                    {
                        con.Close();
                        TempData["MensajeExito"] = "Se ha registrado correctamente";
                        return RedirectToAction("Admin", "Producto");
                    }
                    else
                    {
                        ViewData["MensajeError"] = "Hubo un error";
                        reader.Close();
                        con.Close();
                        return View();
                    }
                }
                else
                {
                    ViewData["MensajeError"] = "Ingrese otra identificación o correo";
                    reader.Close();
                    con.Close();
                    return View();
                }

            }
            else
            {
                ViewData["MensajeError"] = "Las contraseñas no son iguales";
                return View();
            }
        }





        public async Task<IActionResult> cerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public bool existeCliente(ViewUsuario usuario)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from usuario where cedula='" + usuario.cedula + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public bool existeCorreo(ViewUsuario usuario)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from usuario where correo='" + usuario.correo + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }
    }
}
