using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;
public class GeneratePdfContext : DbContext
{
    public GeneratePdfContext(DbContextOptions<GeneratePdfContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Persona>()
        .HasOne<Usuario>(p => p.Usuario)
        .WithOne(ad => ad.Persona)
        .HasForeignKey<Usuario>(ad => ad.IdPersonafk);
    }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ProductoVenta> ProductoVentas { get; set; }
    public DbSet<Punto> Puntos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Venta> Ventas { get; set; }
}
