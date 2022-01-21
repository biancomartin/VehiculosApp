using AutoMapper;
using Vehiculos.App.Models;
using Vehiculos.Modelos;
using Vehiculos.Modelos.Dto;
using Vehiculos.Models;

namespace Vehiculos.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Datos, PersonaDto>()
                .ForMember(dest =>
                    dest.Apellido,
                    opt => opt.MapFrom(src => src.last_name))
                .ForMember(dest =>
                    dest.Nombre,
                    opt => opt.MapFrom(src => src.first_name))
                .ForMember(dest =>
                    dest.Email,
                    opt => opt.MapFrom(src => src.email))
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.id));

            CreateMap<VehiculoCreateViewModel, Vehiculo>()
                .ReverseMap();

            CreateMap<VehiculoViewModel, Vehiculo>().ReverseMap();
        }
    }
}
