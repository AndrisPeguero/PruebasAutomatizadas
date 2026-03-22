

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.notificacion;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;

namespace ReservaBook.Core.Aplication.Services
{
    public class NotificacionService : GenericService<CreateNotificacionRequestDto, CreateNotificacionRequestDto, NotificacionResponseDto, Notificacion>, INotificacionService
    {
        private readonly IMapper _mapper;
        private readonly INotificacionRepository repo;
        public NotificacionService(INotificacionRepository repo, IMapper _mapper) : base(repo, _mapper)
        {
            this._mapper = _mapper;
            this.repo = repo;
        }

        public async Task<List<NotificacionResponseDto>> GetNotificacionByReceptorId(string receptorId)
        {
            var response = new NotificacionResponseDto() { HasError = false, Errors = [] };
            var responseList = new List<NotificacionResponseDto>();
            try
            {

                var entities = await repo.GetlAllAsync();

                if(entities == null || entities.Count <= 0)
                {
                    response.HasError = true;
                    response.Errors.Add("No se encontraron notificaciones registrada");
                    responseList.Add(response);
                    return responseList;
                }


                var map = _mapper.Map<List<NotificacionResponseDto>>(entities);
                return map;        


            }catch(Exception ex)
            {

                throw new Exception("Ocurrio un error al obtener las notificaciones" + ex.Message);
              
            }
        }




    }
}
