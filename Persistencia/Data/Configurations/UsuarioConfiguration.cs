using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.Property(p => p.Username).HasColumnType("varchar").HasMaxLength(30).IsRequired();
        builder.Property(p => p.Password).HasColumnType("varchar").HasMaxLength(255).IsRequired();
        builder.Property(p => p.Correo).HasColumnType("varchar").HasMaxLength(255).IsRequired();

    }
}
