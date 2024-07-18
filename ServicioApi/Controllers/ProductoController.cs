using Microsoft.AspNetCore.Mvc;
using ServicioApi.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace ServicioApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductoController : Controller
	{
		SqlConnection con = new SqlConnection();
		SqlCommand cmd = new SqlCommand();
		SqlDataReader reader;

		void connectionString()
		{
			con.ConnectionString = "Data source=P\\SQLEXPRESS; Initial catalog= 'db_tecnoferre'; integrated security= true; TrustServerCertificate=Yes;";
		}

		[HttpGet]
		[Route("Listar")]
		public IActionResult ListarProductos()
		{

			List<Producto> listaProductos = new List<Producto>();

			try
			{

					connectionString();
					con.Open();
					cmd.Connection = con;
					cmd.CommandText = "select * from producto";
					using (var rd = cmd.ExecuteReader())
					{

						while (rd.Read())
						{

							listaProductos.Add(new Producto
							{
								id = Convert.ToInt32(rd["id"]),
								nombre = (rd["nombre"].ToString()),
								categoria = rd["categoria"].ToString(),
								precio = Convert.ToDecimal(rd["precio"])
							});
						}
						con.Close();
				}

				return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listaProductos });
			}
			catch (Exception error)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = listaProductos });

			}
		}
		
		[HttpPost]
		[Route("Guardar")]
		public IActionResult Guardar([FromBody] Producto producto)
		{
			try
			{

				connectionString();
				con.Open();
				cmd.Connection = con;
				cmd.CommandText = "insert into producto(nombre, categoria, precio) values (@nombre, @categoria, @precio)";
				cmd.Parameters.AddWithValue("nombre", producto.nombre);
				cmd.Parameters.AddWithValue("categoria", producto.categoria);
				cmd.Parameters.AddWithValue("precio", producto.precio);

				cmd.ExecuteNonQuery();

				return StatusCode(StatusCodes.Status200OK, new { mensaje = "agregado" });
			}
			catch (Exception error)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

			}
		}
		
		[HttpGet]
		[Route("Obtener/{id:int}")]
		public IActionResult ObtenerProductos(int id)
		{

			List<Producto> listaProducto = new List<Producto>();
			Producto producto = new Producto();

			try
			{

				connectionString();
				con.Open();
				cmd.Connection = con;
				cmd.CommandText = "select * from producto";
				using (var rd = cmd.ExecuteReader())
					{

						while (rd.Read())
						{

							listaProducto.Add(new Producto
							{
								id = Convert.ToInt32(rd["id"]),
								nombre = (rd["nombre"].ToString()),
								categoria = rd["categoria"].ToString(),
								precio = Convert.ToDecimal(rd["precio"])
							});
						}

					}

				producto = listaProducto.Where(item => item.id == id).FirstOrDefault();

				return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = producto });
			}
			catch (Exception error)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = producto });

			}
		}

		[HttpPut]
		[Route("Editar")]
		public IActionResult Editar([FromBody] Producto producto)
		{
			try
			{

				connectionString();
				con.Open();
				cmd.Connection = con;
				cmd.CommandText = "update producto set nombre = @nombre, categoria = @categoria, precio = @precio";

				cmd.Parameters.AddWithValue("@nombre", producto.nombre is null ? DBNull.Value : producto.nombre);
				cmd.Parameters.AddWithValue("@categoria", producto.categoria is null ? DBNull.Value : producto.categoria);
				cmd.Parameters.AddWithValue("@precio", producto.precio == 0 ? DBNull.Value : producto.precio);

				cmd.ExecuteNonQuery();
				

				return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
			}
			catch (Exception error)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

			}
		}

		[HttpDelete]
		[Route("Eliminar/{id:int}")]
		public IActionResult Eliminar(int id)
		{
			try
			{

				connectionString();
				con.Open();
				cmd.Connection = con;
				cmd.CommandText = "delete from producto where id = @id";
				cmd.Parameters.AddWithValue("@id", id);

				cmd.ExecuteNonQuery();

				return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
			}
			catch (Exception error)
			{

				return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

			}
		}


	}

}
