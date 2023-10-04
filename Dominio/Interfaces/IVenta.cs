using Dominio.Entities;
using Dominio.Views;

namespace Dominio.Interfaces;
public interface IVenta : IGeneric<Venta>
{
    Task<IEnumerable<VentasxMes>> GetVentasxMes(int anio);
}
