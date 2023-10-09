using API.Dtos;
using AutoMapper;
using Dominio.Interfaces;
using Dominio.Views;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;


namespace API.Controllers;
public class VentaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;

    public VentaController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
        _environment = environment;
    }
    [HttpGet("GetHistorialVentas/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentasxAnioDto>>> GetHistorialVentas(int anio)
    {
        var ventas = await _unitOfWork.Ventas.GetVentasxAnio(anio);
        return _mapper.Map<List<VentasxAnioDto>>(ventas);
    }
    [HttpGet("GetValorTotalVentas/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TotalVentasxAnioDto>> GetValorTotalVentas(int anio)
    {
        var ventas = await _unitOfWork.Ventas.GetTotalVentasxAnio(anio);
        return _mapper.Map<TotalVentasxAnioDto>(ventas);
    }
    [HttpGet("generatepdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GeneratePDF(int anio)
    {

        var ventas = await _unitOfWork.Ventas.GetVentasxAnio(anio);
        IEnumerable<VentasxAnioDto> datos = _mapper.Map<List<VentasxAnioDto>>(ventas);
        var datosmap = _mapper.Map<List<VentasxAnio>>(datos);

        var ventasTotal = await _unitOfWork.Ventas.GetTotalVentasxAnio(anio);
        TotalVentasxAnioDto datoResumen = _mapper.Map<TotalVentasxAnioDto>(ventasTotal);
        var resumen = _mapper.Map<TotalVentasxAnio>(datoResumen);
        string image = GetBase64string();
        var response = await _unitOfWork.Ventas.GenPdf(datosmap, resumen, image);
        string Filename = "ReporteVentas-" + anio + ".pdf";
        return File(response, "application/pdf", Filename);
    }
    [NonAction]
    public string GetBase64string()
    {
        string filepath = Path.Combine(Directory.GetCurrentDirectory(), "img", "Gatitou.jpg");
        Console.WriteLine(filepath);
        byte[] imgarray = System.IO.File.ReadAllBytes(filepath);
        string base64 = Convert.ToBase64String(imgarray);
        return base64;
    }

}
