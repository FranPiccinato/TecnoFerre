using Microsoft.EntityFrameworkCore;

namespace TecnoFerre.Models
{
    public class ContextoAppDB : DbContext
    {
        public ContextoAppDB(DbContextOptions<ContextoAppDB> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
    }
}