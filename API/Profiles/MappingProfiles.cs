using API.Dtos;
using AutoMapper;
using Dominio.Views;

namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<VentasxAnio, VentasxAnioDto>().ReverseMap();
    }
}
