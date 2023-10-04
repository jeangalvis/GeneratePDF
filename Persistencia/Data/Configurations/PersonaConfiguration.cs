using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;
public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.Property(p => p.NombrePersona).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Apellido).HasColumnType("varchar").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Cedula).HasColumnType("varchar").HasMaxLength(20).IsRequired();

        builder.HasOne(p=>p.Punto)
        .WithMany(p=>p.Personas)
        .HasForeignKey(p=>p.IdPuntofk);
    }
}
