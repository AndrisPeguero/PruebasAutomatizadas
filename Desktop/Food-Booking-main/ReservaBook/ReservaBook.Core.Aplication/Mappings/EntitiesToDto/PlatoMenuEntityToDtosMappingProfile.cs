using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.platoMenu;
using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class PlatoMenuEntityToDtosMappingProfile : Profile
    {


        public PlatoMenuEntityToDtosMappingProfile()
        {


            CreateMap<PlatoMenu, SavePlatoMenuRequesDto>()
                .ForMember(r => r.IdPlatos, opt => opt.MapFrom(src => src.PlatoId))
                .ForMember(r => r.IdMenu, opt => opt.MapFrom(src => src.MenuId))
                .ReverseMap()
                 .ForMember(r => r.PlatoId, opt => opt.MapFrom(src => src.IdPlatos))
                .ForMember(r => r.MenuId, opt => opt.MapFrom(src => src.IdMenu));


            CreateMap<PlatoMenu, PlatoMenuResponseDto>()
                .ReverseMap();






        }



    }
}
