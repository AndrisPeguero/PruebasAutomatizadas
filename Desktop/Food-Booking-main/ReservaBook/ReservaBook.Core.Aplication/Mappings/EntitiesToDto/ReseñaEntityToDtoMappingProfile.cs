

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.Reseña;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class ReseñaEntityToDtoMappingProfile : Profile
    {


        public ReseñaEntityToDtoMappingProfile()
        {

            CreateMap<SaveReseñaRequestDto, CreateReseñaDto>()
                   .ReverseMap();


            CreateMap<UpdateReseñaRequestDto, CreateReseñaDto>()
                 .ReverseMap();


            CreateMap<Reseña, CreateReseñaDto>()
               .ReverseMap();


            CreateMap<Reseña, ReseñaResponseDto>()
               .ReverseMap();

        }

    }
}
