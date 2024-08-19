namespace tecno.Models
{
    public class Factura
    {
        public int id { get; set; }
        public string nFactura { get; set; }
        public int id_usuario { get; set; }
        public Usuario usuario { get; set; }
        public DateTime fechaEmision { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }

        public int id_carrito { get; set; }
        public Cart carrito { get; set; }



        public int? id_mensajero { get; set; }
        public Usuario Mensajero { get; set; }

        public ICollection<FacturaItem> FacturaItems { get; set; }

        public string Estado { get; set; }
    }
}
