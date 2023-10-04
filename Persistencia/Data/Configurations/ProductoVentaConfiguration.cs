using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class ProductoVentaConfiguration : IEntityTypeConfiguration<ProductoVenta>
{
    public void Configure(EntityTypeBuilder<ProductoVenta> builder)
    {
        builder.ToTable("productoVenta");

        builder.Property(p => p.Cantidad).HasColumnType("int").HasMaxLength(10).IsRequired();

        builder.HasOne(p => p.Venta)
        .WithMany(p => p.ProductoVentas)
        .HasForeignKey(p => p.IdVentafk);

        builder.HasOne(p => p.Producto)
        .WithMany(p => p.ProductoVentas)
        .HasForeignKey(p => p.IdProductofk);
    }
}
