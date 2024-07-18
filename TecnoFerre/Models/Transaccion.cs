namespace TecnoFerre.Models
{
    public class Transaccion
    {
        public int TransaccionID { get; set; }
        public int UsuarioID { get; set; }
        public int ProductoID { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; } // Ejemplo: "Activa", "Anulada"

        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
    }
}
