

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.Reseña;
using ReservaBook.Core.Aplication.Dtos.reserva;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class ReservaEntitiyToDtosMappingProfile : Profile
    {
        public ReservaEntitiyToDtosMappingProfile()
        {


           CreateMap<CreateReservaRequestDto, SaveReservaRequestDto>()
                      .ReverseMap();

           CreateMap<CreateReservaRequestDto, UpdateReservaRequestDto>()
                    .ReverseMap();

           CreateMap<Reserva, ReservaResponseDto>()
                   .ReverseMap();


            CreateMap<Reserva, CreateReservaRequestDto>()
              .ReverseMap();

        }


    }
}
