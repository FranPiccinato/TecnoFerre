using Microsoft.EntityFrameworkCore;
using tecno.Models;

namespace tecno.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>().ToTable("producto");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<FacturaItem>().ToTable("FacturaItems");
            // Configurar el mapeo de la entidad CartItem a la tabla 'cartitems', si es necesario
            modelBuilder.Entity<CartItem>().ToTable("CartItems");
 





            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Producto)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<Cart>()
               .HasOne(c => c.Usuario)
               .WithMany()
               .HasForeignKey(c => c.UserId)
               .HasConstraintName("FK_Cart_Usuario")
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasMany(f => f.FacturaItems)
                .WithOne(fi => fi.Factura)
                .HasForeignKey(fi => fi.FacturaId);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.carrito)
                .WithMany()
                .HasForeignKey(f => f.id_carrito)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.usuario)
                .WithMany()
                .HasForeignKey(f => f.id_usuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
              .HasOne(f => f.Mensajero)
              .WithMany()
              .HasForeignKey(f => f.id_mensajero)
              .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
