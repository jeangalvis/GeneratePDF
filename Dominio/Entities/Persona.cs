namespace Dominio.Entities;
public class Persona : BaseEntity
{
    public string NombrePersona { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public int IdPuntofk { get; set; }
    public Punto Punto { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<Venta> Ventas { get; set; }
}
