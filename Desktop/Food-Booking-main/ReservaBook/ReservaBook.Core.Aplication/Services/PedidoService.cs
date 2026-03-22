using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;


namespace ReservaBook.Core.Aplication.Services
{
    public class PedidoService : GenericService<CreatePedidoRequestDto, CreatePedidoRequestDto, PedidoResponseDto, Pedido>, IPedidoService
    {

        private readonly IPedidoRepository _repo;
        private readonly IMesaRepository mesaRepository;
        private readonly IRestauranteRepository restauranteRepository;  
        private readonly INotificacionRepository notificacionRepository;

        public PedidoService(IPedidoRepository _rep, IMesaRepository mesaRepository, IRestauranteRepository restauranteRepository, INotificacionRepository notificacionRepository, IMapper _mapper) : base(_rep, _mapper)
        {

            this._repo = _rep;
            this.restauranteRepository = restauranteRepository;
            this.mesaRepository = mesaRepository;
            this.notificacionRepository = notificacionRepository;


        }







        public override async Task<PedidoResponseDto?> AddAsync(CreatePedidoRequestDto? entity)
        {

            try
            {
                if (entity == null)
                    return null;


                var response = new PedidoResponseDto() { HasError = false, Errors = [] };

                var fechaHoraPedido = entity.Fecha.ToDateTime(entity.Hora);
                var fechaHoraActual = DateTime.Now;

                if (fechaHoraPedido < fechaHoraActual)
                {
                    response.HasError = true;
                    response.Errors.Add("La fecha y hora del pedido debe ser mayor o igual a la fecha actual");
                    return response;
                }




                if (entity.Fecha > DateOnly.FromDateTime(DateTime.MaxValue))
                {
                    response.HasError = true;
                    response.Errors.Add("La fecha introduccioda no es valida, favor verificar otra vez");
                    return response;
                }




                if (entity.Hora > TimeOnly.FromDateTime(DateTime.MaxValue))
                {
                    response.HasError = true;
                    response.Errors.Add("La hora introduccida no es valida,favor verificar");
                    return response;
                }



                var restaurante = await restauranteRepository.GetByIdAsync(entity.IdRestaurante);
                var mesa = await  mesaRepository.GetByIdAsync(entity.IdMesa);    

                if (restaurante == null || mesa == null)
                {
                    response.HasError = true;
                    response.Errors.Add("No se encontro un restaurante relacionado o una mesa, para realizar el pedido, favor verificar");
                    return response;

                }

                var notificacion = new Notificacion()
                {

                    Id = 0,
                    Fecha = DateTime.Now,
                    Tipo = "Realizacion de pedido",
                    Descripcion = "Un cliente realizo un pedido, favor verificar y antender de ser autentico",
                    ReceptorId = restaurante.UsuarioId,
                    SenderId = entity.ClienteId

                };


                await notificacionRepository.AddAsync(notificacion);


                entity.IdRestaurante = restaurante.Id;
                entity.IdMesa = mesa.Id;
                entity.Total = 0;
                entity.Estado = EstadoPedido.Pendiente;
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }

        }







        public async Task<bool> ChangeStatus(int IdPedido,  EstadoPedido estado)
        {
            try
            {
                if (IdPedido <= 0)
                {
                    return false;
                }

                if (estado <= 0)
                {
                    return false;
                }



                await _repo.ChangeStatus(IdPedido, estado);
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error la intentar cambiar el estado de la mesa " + ex.Message);

            }

        }






        public override async Task<PedidoResponseDto?> UpdateAsync(int id, CreatePedidoRequestDto? entity)
        {

            if (entity == null)
            {
                return null;

            }

            var response = new PedidoResponseDto() { HasError = true, Errors = [] };

            var IsExit = await _repo.GetByIdAsync(id);


            if (IsExit == null)
            {
                response.HasError = true;
                response.Errors.Add("No se encontro una pedido con ese id, favor verificar");
                return response;

            }


            var restaurante = await restauranteRepository.GetByIdAsync(entity.IdRestaurante);

            var notificacion = new Notificacion()
            {

                Id = 0,
                Fecha = DateTime.Now,
                Tipo = "Realizacion de pedido",
                Descripcion = "Un cliente realizo un pedido, favor verificar y antender de ser autentico",
                ReceptorId = restaurante!.UsuarioId,
                SenderId = entity.ClienteId

            };


            await notificacionRepository.AddAsync(notificacion);



            entity!.Id = IsExit.Id;
            entity.Estado = IsExit.Estado;
            entity.IdRestaurante = IsExit.IdRestaurante;    
            entity.IdMesa = IsExit.IdMesa;
            entity.Total = IsExit.Total;
            return await base.UpdateAsync(id, entity);

        }



    }
}
