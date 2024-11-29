using AutoMapper;
using ProyectoEvoltis.Application.DTO;
using ProyectoEvoltis.Domain.Entity;


namespace ProyectoEvoltis.Application.Main.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
