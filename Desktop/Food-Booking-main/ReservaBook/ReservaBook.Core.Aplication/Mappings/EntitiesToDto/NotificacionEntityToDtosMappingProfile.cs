

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.notificacion;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Mappings.EntitiesToDto
{
    public class NotificacionEntityToDtosMappingProfile : Profile
    {
        public NotificacionEntityToDtosMappingProfile()
        {



            CreateMap<Notificacion, NotificacionResponseDto>()
                .ReverseMap();






        }


    }
}
