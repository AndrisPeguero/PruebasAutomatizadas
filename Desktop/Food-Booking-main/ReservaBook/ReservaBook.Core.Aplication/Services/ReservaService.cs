

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.reserva;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;

namespace ReservaBook.Core.Aplication.Services
{
    public class ReservaService : GenericService<CreateReservaRequestDto, CreateReservaRequestDto, ReservaResponseDto, Reserva>, IReservaRestauranteService
    {
        private readonly IMapper _mapper;
        private readonly IReservaResporitory _reservaRepository;
        private readonly IMesaRepository mesaRepository;
        private readonly INotificacionRepository _notificacionRepo;
        private readonly IRestauranteRepository restauranteRepository;
        public ReservaService(IReservaResporitory _reservaRepository, IMesaRepository mesaRepository, INotificacionRepository _notificacionRepo, IRestauranteRepository restauranteRepository, IMapper _mapper) : base(_reservaRepository, _mapper)
        {
            this._mapper = _mapper;
            this._reservaRepository = _reservaRepository; 
            this.mesaRepository = mesaRepository;
            this._notificacionRepo = _notificacionRepo;
            this.restauranteRepository = restauranteRepository;


        }




        public override async Task<ReservaResponseDto?> AddAsync(CreateReservaRequestDto? entity)
        {
               var response = new ReservaResponseDto() { HasError = false, Errors = []};

            try
            {

                if (entity == null)
                {

                    return null;
                }

                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var fecha = entity.Fecha;

                if (fecha < currentDate)
                {
                    response.HasError = false;
                    response.Errors.Add("La fecha de la reserva no puede ser menor a la fecha actual");
                    return response;

                }

                var fechaReserva = entity.Fecha.ToDateTime(entity.Hora);
                var date = DateTime.Now;
                if(fechaReserva < date)
                {
                    response.HasError = false;
                    response.Errors.Add("La fecha de la reserva debe ser a futuro");
                    return response;

                }


                if(entity.IdMesa <= 0)
                {
                    response.HasError = true;
                    response.Errors.Add("Debes indicar la mesa para reservar");
                    return response;
                }

                var mesa = await mesaRepository.GetByIdAsync(entity.IdMesa);

                if(mesa == null)
                {

                    response.HasError = true;
                    response.Errors.Add("No se encontro la mesa especificada, favor revisar");
                    return response;

                }

                mesa.Estado = Estado.NoDisponible.ToString();
                await mesaRepository.UpdateAsync(mesa.Id,mesa);


                if (entity.IdRestaurante <= 0)
                {
                    response.HasError = true;
                    response.Errors.Add("Debes especificar el restaurante donde quieres reservar");
                    return response;
                }

                var restaurante = await restauranteRepository.GetByIdAsync(entity.IdRestaurante);

                var notificacionDuenio = new Notificacion()
                { Id = 0, 
                  Fecha = DateTime.Now, 
                  Descripcion = "Se realizo una reserva correctamente por parte un cliente, favor verificar y atender de ser correspondiente y autentica la reserva", 
                  Tipo = "Reserva de mesa",
                  SenderId = entity.IdUsuario,
                  ReceptorId = restaurante!.UsuarioId}
                ;

                await _notificacionRepo.AddAsync(notificacionDuenio);


                entity.Estado = EstadoSolicitudes.Pendiente.ToString();
              
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar realizar la reserva " + ex.Message);
           
            }

        }


        public async Task<List<ReservaResponseDto>?> GetAllReservaByIdUsuario(string IdUsuario)
        {
            var response = new ReservaResponseDto() { HasError = true, Errors = [] };
            var listResponse = new List<ReservaResponseDto>();

            try
            {
                if (string.IsNullOrWhiteSpace(IdUsuario))
                {
                    return [];
                }


                var entities = await _reservaRepository.GetAllReservaByIdUsuario(IdUsuario);

                if (entities == null || entities.Count == 0) 
                {
                    response.HasError= true;
                    response.Errors.Add("No se encontraron reserva registradas, favor verificar");
                    listResponse.Add(response);
                    return listResponse;
                   
                }

                var map = _mapper.Map<List<ReservaResponseDto>>(entities);
                return map;
                    
            }
            catch (Exception ex)
            {


                throw new Exception("Ocurrio un error al intentar obtener las reservas " +  ex.Message);
            
       
            }
        }




        public override async Task<ReservaResponseDto?> UpdateAsync(int id,CreateReservaRequestDto? entity)
        {
            var response = new ReservaResponseDto() { HasError = false, Errors = [] };

            try
            {

                if (entity == null)
                {

                    return null;
                }

                if(id <= 0)
                {
                    response.HasError = false;
                    response.Errors.Add("Debes indicar un id valido para actualizar");
                    return response;
                }

                var IsExite = await _reservaRepository.GetByIdAsync(id);
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var fecha = entity.Fecha;

                if (fecha < currentDate)
                {
                    response.HasError = false;
                    response.Errors.Add("La fecha de la reserva no puede ser menor a la fecha actual");
                    return response;

                }


                var fechaReserva = entity.Fecha.ToDateTime(entity.Hora);
                var date = DateTime.Now;
                if (fechaReserva < date)
                {
                    response.HasError = false;
                    response.Errors.Add("La fecha de la reserva debe ser a futuro");
                    return response;

                }


                if (entity.IdMesa <= 0)
                {
                    response.HasError = true;
                    response.Errors.Add("Debes indicar la mesa para reservar");
                    return response;
                }


                var restaurante = await restauranteRepository.GetByIdAsync(IsExite!.IdRestaurante);


                var notificacionDuenio = new Notificacion()
                {
                    Id = 0,
                    Fecha = DateTime.Now,
                    Descripcion = "Un cliente actualizo los datos de una reserva, favor verificar y anteder de ser authentica la reserva",
                    Tipo = "Reserva de mesa",
                    SenderId = entity.IdUsuario,
                    ReceptorId = restaurante!.UsuarioId
                };


                entity.Estado = IsExite!.Estado;
                entity.IdRestaurante = IsExite.IdRestaurante;
                entity.IdMesa = IsExite.IdMesa;
                entity.IdUsuario = IsExite.IdUsuario;
                entity.Id = IsExite.Id;
                entity.IdUsuario = IsExite.IdUsuario;

                return await base.UpdateAsync(entity.Id,entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar realizar la reserva " + ex.Message);
            }

        }







    }
}
