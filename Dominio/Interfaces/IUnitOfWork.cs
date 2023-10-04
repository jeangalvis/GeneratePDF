namespace Dominio.Interfaces;
public interface IUnitOfWork
{
    IPersona Personas { get; }
    IProducto Productos { get; }
    IProductoVenta ProductoVentas { get; }
    IPunto Puntos { get; }
    IUsuario Usuarios { get; }
    IVenta Ventas { get; }
    Task<int> SaveAsync();
}
