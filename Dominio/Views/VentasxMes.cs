using Dominio.Entities;

namespace Dominio.Views;
public class VentasxMes : BaseEntity
{
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
    public DateTime FechaVenta { get; set; }
    public int IdPersonafk { get; set; }
    public decimal Total { get; set; }
}
