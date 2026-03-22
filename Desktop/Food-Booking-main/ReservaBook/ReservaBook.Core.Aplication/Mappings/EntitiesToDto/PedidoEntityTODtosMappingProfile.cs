

using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class PedidoEntityTODtosMappingProfile : Profile
    {

        public PedidoEntityTODtosMappingProfile()
        {

            CreateMap<UpdatePedidoRequestDto,CreatePedidoRequestDto>()
                .ReverseMap();



            CreateMap<SavePedidoRequestDto, CreatePedidoRequestDto>()
                .ReverseMap();


            CreateMap<Pedido, CreatePedidoRequestDto>()
                .ReverseMap();



            CreateMap<Pedido, PedidoResponseDto>()
                .ReverseMap();

        }


    }
}
