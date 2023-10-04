namespace Dominio.Entities;
public class Venta : BaseEntity
{
    public DateTime FechaVenta { get; set; }
    public int IdPersonafk { get; set; }
    public Persona Persona { get; set; }
}
