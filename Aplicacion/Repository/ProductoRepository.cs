using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class ProductoRepository : GenericRepository<Producto>, IProducto
{
    private readonly GeneratePdfContext _context;
    public ProductoRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }
}
