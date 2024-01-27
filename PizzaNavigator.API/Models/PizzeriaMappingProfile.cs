using AutoMapper;
using PizzaNavigator.API.Models.Dto;
using PizzaNavigator.API.Models.Entities;

namespace PizzaNavigator.API.Models
{
    public class PizzeriaMappingProfile : Profile
    {
        public PizzeriaMappingProfile()
        {
            CreateMap<Pizzeria, InfoPizzeriaDto>()
                .ForMember(i => i.Street, p => p.MapFrom(s => s.Address.Street))
                .ForMember(i => i.PostalCode, p => p.MapFrom(s => s.Address.PostalCode))
                .ForMember(i => i.City, p => p.MapFrom(s => s.Address.City))
                .ForMember(i => i.User, p => p.MapFrom(s => s.User.Username));

            CreateMap<Pizza, InfoPizzaDto>();
        }
    }
}
