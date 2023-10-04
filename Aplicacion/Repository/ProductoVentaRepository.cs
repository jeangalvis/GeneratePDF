using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class ProductoVentaRepository : GenericRepository<ProductoVenta>, IProductoVenta
{
    private readonly GeneratePdfContext _context;
    public ProductoVentaRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }
}
