using Dominio.Entities;
using Dominio.Interfaces;
using Dominio.Views;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Aplicacion.Repository;
public class VentaRepository : GenericRepository<Venta>, IVenta
{
    private readonly GeneratePdfContext _context;
    public VentaRepository(GeneratePdfContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VentasxAnio>> GetVentasxAnio(int anio)
    {
        DateTime fechaInicio = new DateTime(anio, 1, 1);
        DateTime fechaFinal = new DateTime(anio, 12, 31);
        return await (
                from per in _context.Personas
                join v in _context.Ventas on per.Id equals v.IdPersonafk
                join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
                join p in _context.Productos on pv.IdProductofk equals p.Id
                where v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFinal
                select new VentasxAnio
                {
                    NombreProducto = p.NombreProducto,
                    Cantidad = pv.Cantidad,
                    FechaVenta = v.FechaVenta,
                    IdPersonafk = per.Id,
                    Total = pv.Cantidad * p.Precio
                }
        ).ToListAsync();
    }

    public Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos)
    {
        var document = new PdfDocument();
        string HtmlContent = "<table>";
        HtmlContent+="<tr>";
        HtmlContent+="<th>ID</th>";
        HtmlContent+="<th>NombreProducto</th>";
        HtmlContent+="<th>Cantidad</th>";
        HtmlContent+="</tr>";
        HtmlContent+= "</table>";
        
        if (datos.Any())
        {
            foreach (var dato in datos)
            {
                HtmlContent += "<p>" + dato.NombreProducto + "</p>";
            }
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
        }
        else
        {
            HtmlContent += "<p></p>";
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
        }

        byte[]? response;
        using (MemoryStream ms = new MemoryStream())
        {
            document.Save(ms);
            response = ms.ToArray();
        }

        return Task.FromResult(response);
    }
}
