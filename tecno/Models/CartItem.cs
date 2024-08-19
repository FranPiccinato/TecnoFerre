namespace tecno.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Producto Producto { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
