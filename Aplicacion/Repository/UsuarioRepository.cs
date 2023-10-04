using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;
public class UsuarioRepository : GenericRepository<Usuario>, IUsuario
{
    private readonly GeneratePdfContext _context;
    public UsuarioRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }
}
