
namespace TecnoFerre.Models
{
	public class Producto
	{
		public int id { get; set; }
		public string nombre { get; set; }
		public string categoria { get; set; }
		public decimal precio { get; set; }

        public static implicit operator Producto?(ResultadoApi? v)
        {
            throw new NotImplementedException();
        }
    }
}
