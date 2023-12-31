using Dominio.Entities;
using Dominio.Views;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.Interfaces;
public interface IVenta : IGeneric<Venta>
{
    Task<IEnumerable<VentasxAnio>> GetVentasxAnio(int anio);
    Task<TotalVentasxAnio> GetTotalVentasxAnio(int anio);
    Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos, TotalVentasxAnio resumen, string image);
}
