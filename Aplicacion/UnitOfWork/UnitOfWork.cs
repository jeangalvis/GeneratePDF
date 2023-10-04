using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GeneratePdfContext context;
    public IPersona _personas;
    public IProducto _productos;
    public IProductoVenta _productoVentas;
    public IPunto _puntos;
    public IUsuario _usuarios;
    public IVenta _ventas;

    public UnitOfWork(GeneratePdfContext _context)
    {
        context = _context;
    }
    public IPersona Personas
    {
        get
        {
            if (_personas == null)
            {
                _personas = new PersonaRepository(context);
            }
            return _personas;
        }
    }

    public IProducto Productos
    {
        get
        {
            if (_productos == null)
            {
                _productos = new ProductoRepository(context);
            }
            return _productos;
        }
    }

    public IProductoVenta ProductoVentas
    {
        get
        {
            if (_productoVentas == null)
            {
                _productoVentas = new ProductoVentaRepository(context);
            }
            return _productoVentas;
        }
    }

    public IPunto Puntos
    {
        get
        {
            if (_puntos == null)
            {
                _puntos = new PuntoRepository(context);
            }
            return _puntos;
        }
    }

    public IUsuario Usuarios
    {
        get
        {
            if (_usuarios == null)
            {
                _usuarios = new UsuarioRepository(context);
            }
            return _usuarios;
        }
    }

    public IVenta Ventas
    {
        get
        {
            if (_ventas == null)
            {
                _ventas = new VentaRepository(context);
            }
            return _ventas;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}
