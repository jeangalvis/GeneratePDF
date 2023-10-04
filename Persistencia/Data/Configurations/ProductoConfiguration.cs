using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("producto");

        builder.Property(p => p.NombreProducto).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Stock).HasColumnType("int").HasMaxLength(10).IsRequired();
        builder.Property(p => p.Precio).IsRequired();
    }
}
