
using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.plato;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class PlatoEntityToDtosMappingProfile : Profile
    {

        public PlatoEntityToDtosMappingProfile()
        {

            CreateMap<CreatePlatoRequestDto, SavePlatoRequestDto>()
                .ReverseMap()
                .ForMember(p => p.Imagen, opt => opt.Ignore());



            CreateMap<CreatePlatoRequestDto, UpdatePlatoRequestDto>()
                .ReverseMap()
                .ForMember(p => p.Imagen, opt => opt.Ignore()); 


            CreateMap<Plato, CreatePlatoRequestDto>()
                .ReverseMap();


            CreateMap<Plato,PlatoResponseDto>()
                .ReverseMap();  


         
        
        
        }


    }
}
