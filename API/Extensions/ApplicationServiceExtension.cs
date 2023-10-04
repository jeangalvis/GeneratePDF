using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.UnitOfWork;
using Dominio.Interfaces;

namespace API.Extensions;
public static class ApplicationServiceExtension
{
    public static void ConfigureCors(this IServiceCollection services) =>
       services.AddCors(options =>
       {
           options.AddPolicy("CorsPolicy", builder =>
           builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           );
       });
    public static void AddAplicacionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

}
