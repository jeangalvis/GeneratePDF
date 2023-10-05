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

    public VentaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("GetHistorialVentas/{anio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentasxAnioDto>>> GetHistorialVentas(int anio)
    {
        var ventas = await _unitOfWork.Ventas.GetVentasxAnio(anio);
        return _mapper.Map<List<VentasxAnioDto>>(ventas);
    }
    [HttpGet("generatepdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GeneratePDF(string DocName, int anio)
    {

        var ventas = await _unitOfWork.Ventas.GetVentasxAnio(anio);
        IEnumerable<VentasxAnioDto> datos = _mapper.Map<List<VentasxAnioDto>>(ventas);
        var datosmap = _mapper.Map<List<VentasxAnio>>(datos);
        var response = await _unitOfWork.Ventas.GenPdf(datosmap);
        string Filename = DocName + ".pdf";
        return File(response, "application/pdf", Filename);
    }
}
