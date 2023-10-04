using Dominio.Entities;
using Dominio.Interfaces;
using Dominio.Views;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;
public class VentaRepository : GenericRepository<Venta>, IVenta
{
    private readonly GeneratePdfContext _context;
    public VentaRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VentasxMes>> GetVentasxMes(int anio)
    {
        return await (
                from per in _context.Personas
                join v in _context.Ventas on per.Id equals v.IdPersonafk
                join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                join p in _context.Productos on pv.IdProductofk equals p.Id
                select new VentasxMes
                {
                    NombreProducto = p.NombreProducto,
                    Cantidad = pv.Cantidad,
                    FechaVenta = v.FechaVenta,
                    IdPersonafk = per.Id,
                    Total = pv.Cantidad * p.Precio
                }
        ).ToListAsync();
    }
}
