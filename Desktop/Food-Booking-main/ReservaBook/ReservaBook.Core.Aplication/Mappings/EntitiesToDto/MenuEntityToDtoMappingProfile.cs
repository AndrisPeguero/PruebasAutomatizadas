using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.menu;
using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class MenuEntityToDtoMappingProfile : Profile
    {

        public MenuEntityToDtoMappingProfile() 
        {

            CreateMap<Menu, CreateMenuDto>()
                .ReverseMap();


            CreateMap<Menu, MenuResponseDto>()
               .ReverseMap();


            CreateMap<CreateMenuDto, SaveMenuRequestDto>()
             .ReverseMap();


            CreateMap<CreateMenuDto, UpdateMenuRequestDto>()
            .ReverseMap();

        }



    }
}
