namespace Dominio.Entities;
public class Punto : BaseEntity
{
    public string NombrePunto { get; set; }
    public ICollection<Persona> Personas { get; set; }
}
