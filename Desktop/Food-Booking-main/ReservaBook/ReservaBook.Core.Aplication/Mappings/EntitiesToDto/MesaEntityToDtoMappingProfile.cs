

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Domain.Entities;
using System.Runtime.InteropServices;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class MesaEntityToDtoMappingProfile : Profile
    {

        public MesaEntityToDtoMappingProfile()
        {

            CreateMap<SaveMesaRequestDto, CreateMesaRequestDto>()
                .ReverseMap();



            CreateMap<UpdateMesaRequestDto, CreateMesaRequestDto>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ReverseMap();
                
               


            CreateMap<Mesa, CreateMesaRequestDto>()
                .ReverseMap();
               


            CreateMap<Mesa,MesaResponseDto>()
                .ReverseMap();

        }

    }
}
