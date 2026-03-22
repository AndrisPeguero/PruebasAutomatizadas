

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.pago;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class PagoEntityToDtosMappingProfile : Profile
    {

        public PagoEntityToDtosMappingProfile()
        {

            CreateMap<SavePagoRequestDto, CreatePagoRequesDto>()
                .ReverseMap();


            CreateMap<Pago, CreatePagoRequesDto>()
                .ReverseMap();


            CreateMap<Pago, PagoResponseDto>()
              .ReverseMap();


        }


    }
}
