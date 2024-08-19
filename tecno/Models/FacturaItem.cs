namespace tecno.Models
{
    public class FacturaItem
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
