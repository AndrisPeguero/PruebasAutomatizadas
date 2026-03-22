
using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.pdidoPlato;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class PedidoPlatoEntityToDtosMappingProfile : Profile
    {

        public PedidoPlatoEntityToDtosMappingProfile()
        {

            CreateMap<PedidoPlato, PedidosPlatoResponseDto>()
                .ForMember(p => p.SubTotal, opt => opt.MapFrom(src => src.PrecioUnitario * src.CantidadPlatos))
                      .ReverseMap();



            CreateMap<CreatePedidoPlatoDto, SavePedidoPlatoRequestDto>()
                    .ReverseMap();


            CreateMap<PedidoPlato, CreatePedidoPlatoDto>().ReverseMap();

            CreateMap<CreatePedidoPlatoDto, UpdatePedidoPlatoRequestDto>()
                    .ReverseMap();


        }



    }
}
