namespace Dominio.Entities;
public class Producto : BaseEntity
{
    public string NombreProducto { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }
    public ICollection<ProductoVenta> ProductoVentas { get; set; }
}
