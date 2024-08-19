namespace tecno.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
