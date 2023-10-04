using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class VentaRepository : GenericRepository<Venta>, IVenta
{
    private readonly GeneratePdfContext _context;
    public VentaRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }
}
