using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("venta");

        builder.Property(p => p.FechaVenta).IsRequired();

        builder.HasOne(p => p.Persona)
        .WithMany(p => p.Ventas)
        .HasForeignKey(p => p.IdPersonafk);
    }
}
