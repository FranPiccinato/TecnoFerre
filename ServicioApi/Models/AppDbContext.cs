using Microsoft.EntityFrameworkCore;
using ServicioApi.Models;

namespace ServicioApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("producto");
        }
    }
}
