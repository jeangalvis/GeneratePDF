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
    public async Task<TotalVentasxAnio> GetTotalVentasxAnio(int anio)
    {
        DateTime fechaInicio = new DateTime(anio, 1, 1);
        DateTime fechaFinal = new DateTime(anio, 12, 31);

        var resultado = await (
            from per in _context.Personas
            join v in _context.Ventas on per.Id equals v.IdPersonafk
            join pv in _context.ProductoVentas on v.Id equals pv.IdVentafk
            join p in _context.Productos on pv.IdProductofk equals p.Id
            where v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFinal
            select new
            {
                CantidadVendida = pv.Cantidad,
                TotalVenta = pv.Cantidad * p.Precio
            }
        ).ToListAsync();

        int cantidadTotal = resultado.Sum(r => r.CantidadVendida);
        decimal totalVentas = resultado.Sum(r => r.TotalVenta);

        return new TotalVentasxAnio
        {
            CantidadVendida = cantidadTotal,
            Total = totalVentas
        };
    }

    public Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos, TotalVentasxAnio resumen, string image)
    {
        var document = new PdfDocument();
        string HtmlContent = "<div style='width:100%; text-align:center; margin-bottom:20px'> ";
        string imgurl = "data:image/png;base64, " + image + "";
        HtmlContent += "<img style='width:100px; height:80%' src='" + imgurl + "'>";
        HtmlContent += "<h2>Reporte de Ventas</h2>";
        HtmlContent += "<h6>Fecha del reporte: " + DateTime.Now + "</h6>";
        HtmlContent += "</div>";

        HtmlContent += "<table class='default' style='width:100%; border: 1px solid #000'>";
        HtmlContent += "<thead style='font-weight:bold'>";
        HtmlContent += "<tr>";
        HtmlContent += "<th style='border: 1px solid #000'>NombreProducto</th>";
        HtmlContent += "<th style='border: 1px solid #000'>Cantidad</th>";
        HtmlContent += "<th style='border: 1px solid #000'>FechaVenta</th>";
        HtmlContent += "<th style='border: 1px solid #000'>IdEmpleado</th>";
        HtmlContent += "<th style='border: 1px solid #000'>Total</th>";
        HtmlContent += "</tr>";
        HtmlContent += "</thead>";
        if (datos.Any())
        {

            foreach (var dato in datos)
            {
                HtmlContent += "<tr>";
                HtmlContent += "<td style='text-align:center'>" + dato.NombreProducto + "</td>";
                HtmlContent += "<td style='text-align:center'>" + dato.Cantidad + "</td>";
                HtmlContent += "<td style='text-align:center'>" + dato.FechaVenta + "</td>";
                HtmlContent += "<td style='text-align:center'>" + dato.IdPersonafk + "</td>";
                HtmlContent += "<td style='text-align:center'>" + dato.Total.ToString("N2") + "</td>";
                HtmlContent += "</tr>";
            }
            HtmlContent += "</table>";
            HtmlContent += "</div>";

            if (!resumen.CantidadVendida.Equals(0) && !resumen.Total.Equals(0))
            {
                HtmlContent += "<div style='width:100%; text-align:right; margin-bottom: 10px'>";
                HtmlContent += "<h2>Informacion Resumen de ventas</h2>";

                HtmlContent += "<table style='border: 1px solid #000; float: right'>";
                HtmlContent += "<thead style='font-weight:bold'>";
                HtmlContent += "<tr>";
                HtmlContent += "<th style='border: 1px solid #000'>Cantidad_Vendidos</th>";
                HtmlContent += "<th style='border: 1px solid #000'>Total_Dinero</th>";
                HtmlContent += "</tr>";
                HtmlContent += "</thead>";

                HtmlContent += "<tr>";
                HtmlContent += "<td style='text-align:center''>" + resumen.CantidadVendida + "</td>";
                HtmlContent += "<td style='text-align:center'>" + resumen.Total.ToString("N2") + "</td>";
                HtmlContent += "</tr>";
                HtmlContent += "</table>";
                HtmlContent += "</div>";
            }
            PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
        }
        else
        {
            HtmlContent += "</table>";
            HtmlContent += "</div>";

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
