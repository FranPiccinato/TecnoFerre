using Microsoft.AspNetCore.Mvc;
using TecnoFerre.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace TecnoFerre.Controllers
{
    public class RegistrarController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        void connectionString()
        {
            con.ConnectionString = "Data source=P\\SQLEXPRESS; Initial catalog= 'tecnoferreP'; integrated security= true; TrustServerCertificate=Yes;";
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Cliente cliente)
        {
            if(cliente.correo == "admin@gmail.com")
            {
                connectionString();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select * from admin where correo='" + cliente.correo + "' and contrasena='" + cliente.contrasena + "'";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return RedirectToAction("Admin", "Producto");
                }
                else
                {
                    ViewData["Mensaje"] = "No se encontró el usuario";
                    con.Close();
                    return View();
                }
            }
            else
            {
                connectionString();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select * from cliente where correo='" + cliente.correo + "' and contrasena='" + cliente.contrasena + "'";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mensaje"] = "No se encontró el usuario";
                    con.Close();
                    return View();
                }
            }
        }

        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrarse(Cliente cliente)
        {
            if(cliente.contrasena == cliente.confirmarContrasena)
            {
                bool existeCl = existeCliente(cliente);
                bool existeCo = existeCorreo(cliente);
                if (!existeCl && !existeCo)
                {
                    connectionString();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = string.Format("insert into cliente values({0}, '{1}', '{2}', '{3}', '{4}')", cliente.cedula, cliente.nombre, cliente.correo, cliente.contrasena, cliente.direccion);
                    int clienteNuevo = (int)cmd.ExecuteNonQuery();
                    if (clienteNuevo > 0)
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
        public IActionResult cambiarContra(Cliente cl)
        {
            return View();
        }
        public bool existeCliente(Cliente cliente)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from cliente where cedula='" + cliente.cedula + "'";
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                con.Close();
                return true;
            }
            con.Close();
            return false;
        }

        public bool existeCorreo(Cliente cliente)
        {
            connectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from cliente where correo='" + cliente.correo + "'";
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
