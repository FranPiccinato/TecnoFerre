using System.ComponentModel.DataAnnotations;

namespace tecno.Models
{
    public class Usuario
    {
        [Key]
        public int cedula {  get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public string direccion {  get; set; }
        public int rol {  get; set; }
    }
}
