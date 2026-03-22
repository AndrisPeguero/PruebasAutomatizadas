
using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class RestauranteEntityToDtoMappingProfile : Profile
    {

        public RestauranteEntityToDtoMappingProfile()
        {


            CreateMap<CreateRestauranteRequestDto, SaveRestauranteRequestDto>()
                .ForMember(r => r.Imagen, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(r => r.Imagen, opt => opt.Ignore());


            CreateMap<CreateRestauranteRequestDto, UpdateRestauranteRequestDto>()
                .ForMember(r => r.Imagen, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(r => r.Imagen, opt => opt.Ignore());

            CreateMap<Restaurante, CreateRestauranteRequestDto>()
                .ReverseMap();

            CreateMap<Restaurante, RestauranteResponseDto>()
                .ReverseMap();

        }


    }
}
