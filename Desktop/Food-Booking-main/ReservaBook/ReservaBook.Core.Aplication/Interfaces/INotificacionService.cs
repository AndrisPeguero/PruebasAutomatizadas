using ReservaBook.Core.Aplication.Dtos.notificacion;
using ReservaBook.Core.Domain.Entities;
using System;


namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface INotificacionService : IGenericService<CreateNotificacionRequestDto, CreateNotificacionRequestDto,NotificacionResponseDto,Notificacion>
    {
        Task<List<NotificacionResponseDto>> GetNotificacionByReceptorId(string receptorId);
    }
}
