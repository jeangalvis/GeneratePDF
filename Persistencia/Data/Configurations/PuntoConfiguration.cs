using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class PuntoConfiguration : IEntityTypeConfiguration<Punto>
{
    public void Configure(EntityTypeBuilder<Punto> builder)
    {
        builder.ToTable("punto");

        builder.Property(p => p.NombrePunto).HasColumnType("varchar").HasMaxLength(100).IsRequired();
    }
}
