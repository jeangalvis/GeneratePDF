using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class PuntoRepository : GenericRepository<Punto>, IPunto
{
    private readonly GeneratePdfContext _context;
    public PuntoRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }
}
