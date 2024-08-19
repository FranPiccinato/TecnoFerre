
namespace tecno.Models
{
	public class Producto
	{
		public int id { get; set; }
		public string nombre { get; set; }
		public string categoria { get; set; }
		public decimal precio { get; set; }
        public string descripcion { get; set; }
        public string imagen { get; set; }
        public DateTime fecha_ingreso { get; set; }

        public static implicit operator Producto?(ResultadoApi? v)
        {
            throw new NotImplementedException();
        }
    }
}
