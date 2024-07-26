namespace TecnoFerre.Models
{
    public class Usuario
    {
        public int cedula {  get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string confirmarContrasena { get; set; }
        public string direccion {  get; set; }
        public int rol {  get; set; }
    }
}
