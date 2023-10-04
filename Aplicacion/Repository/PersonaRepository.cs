using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class PersonaRepository : GenericRepository<Persona>, IPersona
{
    private readonly GeneratePdfContext _context;
    public PersonaRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }
}
