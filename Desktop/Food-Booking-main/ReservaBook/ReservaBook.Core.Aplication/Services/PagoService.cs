

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.pago;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using System.Net.Http.Headers;

namespace ReservaBook.Core.Aplication.Services
{
    public class PagoService : GenericService<CreatePagoRequesDto, CreatePagoRequesDto,PagoResponseDto,Pago>, IPagoService
    {

        private readonly IMapper _mapper;
        private readonly IPagoRepository _pago;
        private readonly INotificacionRepository notificacionRepository;
        private readonly IRestauranteRepository restauranteRepository;
        private readonly IPedidoRepository pedidoRepo;

            


        public PagoService(IMapper _mapper, IPagoRepository _pago, INotificacionRepository notificacionRepository, IRestauranteRepository restauranteRepository, IPedidoRepository pedidoRepo) : base(_pago,_mapper)
        {
            this._mapper = _mapper;
            this._pago = _pago;
            this.notificacionRepository = notificacionRepository;
            this.pedidoRepo = pedidoRepo;
            this.restauranteRepository = restauranteRepository;
        }




        public override async Task<PagoResponseDto?> AddAsync(CreatePagoRequesDto? entity)
        {

            if (entity == null)
            {

                return null;

            }

            entity.Id = 0;
            entity.Estado = EstadoPago.pendiente.ToString();
            entity.Fecha = DateTime.Now;


            var pedido = await pedidoRepo.GetByIdAsync(entity.IdPedido);
            var restaurante = await restauranteRepository.GetByIdAsync(pedido!.IdRestaurante);

            var notificacion = new Notificacion()
            {
                Id = 0,
                Fecha = DateTime.Now,

                Tipo = "Pago de pedido",
                Descripcion = "Un cliente realizo el pago de un pedido, favor verificar y comunicarlo si no es authentico",
                SenderId = entity.UsuarioId,
                ReceptorId = restaurante!.UsuarioId
            };


            await notificacionRepository.AddAsync(notificacion);
           
           
            return await base.AddAsync(entity);

        }





        public async Task<PagoResponseDto?> GetBypedidoId(int pedidoId)
        {
               var response = new PagoResponseDto() { HasError = false, Errors = []};

            try
            {
                if(pedidoId == 0)
                {
                    return null;   

                }


                var entity = await _pago.GetByIdPedido(pedidoId);


                if(entity == null)
                {
                    response.HasError = true;
                    response.Errors.Add("No se encontro el pago especificado");
                    return response;

                }


                var map = _mapper.Map<PagoResponseDto>(entity);
                return map;

            }
            catch (Exception ex) 
            {
                throw new Exception("Ocurrio un error al obtener el pago por el id del pedido" + ex.Message);
            
            
            }
        }

       
    }
}
