namespace Dominio.Entities;
public class ProductoVenta : BaseEntity
{
    public int Cantidad { get; set; }
    public int IdVentafk { get; set; }
    public Venta Venta { get; set; }
    public int IdProductofk { get; set; }
    public Producto Producto { get; set; }

}
