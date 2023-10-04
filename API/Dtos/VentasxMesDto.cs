namespace API.Dtos;
public class VentasxMesDto
{
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
    public DateTime FechaVenta { get; set; }
    public int IdPersonafk { get; set; }
    public decimal Total { get; set; }
}
