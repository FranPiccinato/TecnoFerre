using Microsoft.AspNetCore.Mvc;
using TecnoFerre.Models;
using System.Data.SqlClient;

namespace TecnoFerre.Controllers
{
    public class RegistrarController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        void connectionString()
        {
            con.ConnectionString = "Data source=P\\SQLEXPRESS; Initial catalog= 'db_tecnoferre'; integrated security= true; TrustServerCertificate=Yes;";
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from usuario where correo='" + usuario.correo + "' and contrasena='" + usuario.contrasena + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if ((int)reader["rol"] == 1)
                {
                    con.Close();
                    return RedirectToAction("Admin", "Producto");
                }
                else if ((int)reader["rol"] == 2)
                {
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
                ViewData["Mensaje"] = "No se encontró el usuario";
                con.Close();
                return View();
            }

        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse(Usuario usuario)
        {
            if (usuario.contrasena == usuario.confirmarContrasena)
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
                        return RedirectToAction("Login", "Registrar");
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
        public IActionResult cambiarContra()
        {
            return View();
        }

        [HttpPost]
        public IActionResult cambiarContra(Usuario usuario)
        {
            return View();
        }
        public bool existeCliente(Usuario usuario)
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

        public bool existeCorreo(Usuario usuario)
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