# Generar Pdf con PdfSharpCore utilizando Polibioz

Una vez tenemos nuestro esqueleto del proyecto, en la capa de API agregamos la libreria con el siguiente comando:

`dotnet add package Polybioz.HtmlRenderer.PdfSharp.Core --version 1.0.0`

Nota: Si la información que queremos mostrar proviene de la base de datos, es necesario que exista la consulta de esa información en los controladores.

### Interfaces

En la interfaz sobre la que vamos a trabajar, agregamos una función que reciba un arreglo de bytes y como argumento le pasamos la entidad o la lista de la entidad.

```dotnet
Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos);
```

### Repository

- Implementamos la interfaz.
- Generamos el objeto PdfDocument.
- Agregamos una variable de tipo string que va a contener el contenido que se va a enviar al PDF en formato HTML.
- Agregamos el PdfGenerator y como argumentos pasamos el objeto generado, el contenido de HTML y el tamaño del documento.
- Creamos una variable de tipo arreglo de bytes
- Creamos un MemoryStream, es una secuencia de bytes que se almacena en la RAM
- Agregamos el contenido del pdf al MemoryStream.
- Convertimos el contenido del MemoryStream en un arreglo de bytes.
- Retornamos el arreglo de bytes para que pueda ser usado en una petición HTTP.

Ejemplo del contenido en el repositorio:

```dotnet
public Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos)
{
	var document = new PdfDocument();
	string HtmlContent = <table class='default' style='width:100%; border: 1px solid #000'>";
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
		PdfGenerator.AddPdfPages(document, HtmlContent, PageSize.A4);
	}
	else
	{
		HtmlContent += "</table>";
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
```

### Controllers

- Creamos la función que va a retornar el PDF a la petición HTTP.

```dotnet
public async Task<IActionResult> GeneratePDF(int anio){

}
```

- Obtenemos la información de la función que realiza la consulta que queremos mostrar en el PDF.

```dotnet
public async Task<IActionResult> GeneratePDF(int anio){
	var ventas = await _unitOfWork.Ventas.GetVentasxAnio(anio);
	IEnumerable<VentasxAnioDto> datos = _mapper.Map<List<VentasxAnioDto>>(ventas);
	var datosmap = _mapper.Map<List<VentasxAnio>>(datos);
}
```

- Agregamos la función que genera el PDF y le pasamos como argumento los datos que obtuvimos de la función que hace la consulta a la DB.

```dotnet
var response = await _unitOfWork.Ventas.GenPdf(datosmap);
```

- Creamos una variable string donde se va a especificar el nombre que se le va a asignar el archivo.

```dotnet
string Filename = "ReporteVentas-" + anio + ".pdf";
```

- Retorno del archivo al que se le va a pasar la información obtenida en la función que obtiene el PDF, el tipo de contenido y el nombre del archivo asignado en la variable anterior.

```dotnet
return File(response, "application/pdf", Filename);
```

Ejemplo implementación en controlador:

```dotnet
public async Task<IActionResult> GeneratePDF(int anio)
{

	var ventas = await _unitOfWork.Ventas.GetVentasxAnio(anio);
	IEnumerable<VentasxAnioDto> datos = _mapper.Map<List<VentasxAnioDto>>(ventas);
	var datosmap = _mapper.Map<List<VentasxAnio>>(datos);

	var response = await _unitOfWork.Ventas.GenPdf(datosmap);
	string Filename = "ReporteVentas-" + anio + ".pdf";
	return File(response, "application/pdf", Filename);
}
```

## Agregando Imagenes

Proceso para agregar imagenes al PDF

- Creamos una carpeta "img" en .\API\ y guardamos la imagen con extension .jpg

### Interfaces

Agregamos un argumento a la funcion de tipo string.

```dotnet
Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos, string image);
```

### Repository

- Agregamos el argumento de tipo string en la funcion.

```dotnet
public Task<byte[]> GenPdf(IEnumerable<VentasxAnio> datos, string image){

}
```

- Creamos una variable de tipo string que va a contener la imagen en base64

```dotnet
string imgurl = "data:image/png;base64, " + image + "";
```

- Agregamos en un HtmlContent la variable que contiene la imagen

```dotnet
HtmlContent += "<img style='width:100px; height:80%' src='" + imgurl + "'>";
```

### Controllers

Agregamos una nueva función que va a obtener la ruta donde esta la imagen estatica, la vuelve un arreglo de bytes y luego la convierte a base 64, esto se hace para la compatibilidad, inclusion y facilita la compartición de imagenes , ejemplo:

```dotnet
[NonAction]
public string GetBase64string()
{
	string filepath = Path.Combine(Directory.GetCurrentDirectory(), "img", "nombre_imagen.jpg");
	byte[] imgarray = System.IO.File.ReadAllBytes(filepath);
	string base64 = Convert.ToBase64String(imgarray);
	return base64;
}
```

Finalmente, en nuestra petición Get que crea el PDF, creamos una variable string que va a almacenar lo que retorne la función GetBase64string(), luego agregamos esta variable a la función del repositorio, Ejemplo:

```dotnet
string image = GetBase64string();
var response = await _unitOfWork.Ventas.GenPdf(datosmap, image);
```

###End