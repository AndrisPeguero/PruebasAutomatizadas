using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Aplication.Dtos.pdidoPlato;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Aplication.Dtos.plato;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;



namespace ReservaBook.Core.Aplication.Services
{
    public class PedidoPlatoService : GenericService<CreatePedidoPlatoDto, CreatePedidoPlatoDto, PedidosPlatoResponseDto, PedidoPlato>, IPedidoPlatoService
    {

        private readonly IMapper _mapper;
        private readonly IPedidoPlatoRepository _repo;
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IPlatoRepository platoRepo;


        public PedidoPlatoService(IPedidoPlatoRepository _repo, IMapper mapper, IPedidoRepository _pedidoRepo, IPlatoRepository platoRepo) : base(_repo, mapper)
        {

            this._mapper = mapper;
            this._repo = _repo;
            this._pedidoRepo = _pedidoRepo;
            this.platoRepo = platoRepo;


        }


        public async Task<List<PedidosPlatoResponseDto?>> AddRangeAsync(List<CreatePedidoPlatoDto> dto)
        {
            try
            {
                var listResponse = new List<PedidosPlatoResponseDto>();
                var listMonto = new List<decimal>();
                var listPrecioUnitario = new List<decimal>();
                var response = new PedidosPlatoResponseDto() { HasError = false, Errors = [] };

                if (dto == null || !dto.Any())
                {

                    return [];

                }


                foreach (var item in dto)
                {

                    var entity = await platoRepo.GetByIdAsync(item.IdPlato);
                    if (entity == null)
                    {
                        continue;
                    }

                    listPrecioUnitario.Add(entity.Precio);

                }

                var entities = await platoRepo.GetlAllAsync();

                var listEntities = dto.Select(s => new PedidoPlato()
                {

                    Id = s.Id,
                    IdPedido = s.IdPedido,
                    IdPlato = s.IdPlato,
                    PrecioUnitario = entities.FirstOrDefault(d => d.Id == s.IdPlato)?.Precio ?? 0,
                    CantidadPlatos = s.CantidadPlatos
                }).ToList();


                if (listEntities.Count <= 0 || !listEntities.Any())
                {

                    response.HasError = true;
                    response.Errors.Add("OCurrio un error al intentar agregar los platos al pedido");
                    listResponse.Add(response);
                    return listResponse!;
                }

                foreach (var entity in listPrecioUnitario)
                {
                    listMonto.Add(entity);

                }

                decimal total = await CalcularMonto(listMonto);

                if (total == 0)
                {

                    response.HasError = true;
                    response.Errors.Add("OCurrio un error al intentar calcular el monto total");
                    listResponse.Add(response);
                    return listResponse!;

                }

                var EditPedido = await _pedidoRepo.GetByIdAsync(dto.FirstOrDefault()!.IdPedido);
                EditPedido!.Total = total;
                var pedidoUpdated = await _pedidoRepo.UpdateAsync(EditPedido.Id, EditPedido);
                if (pedidoUpdated == null)
                {
                    response.HasError = true;
                    response.Errors.Add("Ocurrio un error al intentar actualizar el monto total");
                    listResponse.Add(response);
                    return listResponse!;

                }



                await _repo.AddRange(listEntities);
                var map = _mapper.Map<List<PedidosPlatoResponseDto>>(listEntities);
                return map;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar guardar los platos " + ex.Message);

            }
        }





        public override async Task<PedidosPlatoResponseDto?> UpdateAsync(int id, CreatePedidoPlatoDto? entity)
        {

            if (entity == null)
            {
                return null;

            }
           
            var response = new PedidosPlatoResponseDto() { HasError = false, Errors = [] };

            var IsExit = await _repo.GetByIdAsync(id);
   
            if (IsExit == null)
            {
                response.HasError = true;
                response.Errors.Add("No se encontro una un plato con ese id, favor verificar");
                return response;

            }


            var pedido = await _pedidoRepo.GetByIdAsync(IsExit!.IdPedido);
            if (pedido == null)
            {
                response.HasError = true;
                response.Errors.Add("No se encontro una un pedido con ese id, favor verificar");
                return response;

            }

            entity!.Id = IsExit.Id;
            entity.IdPedido = IsExit.IdPedido;
            entity.IdPlato = IsExit.IdPlato;
            entity.PrecioUnitario = IsExit.PrecioUnitario;
            response.SubTotal = entity.PrecioUnitario * entity.CantidadPlatos;

            await base.UpdateAsync(entity.Id, entity);

            var items = await _repo.GetByPedidoId(IsExit.IdPedido);
            decimal total = items.Sum(x => x.PrecioUnitario * x.CantidadPlatos);
            pedido.Total = total;       
            var updatePedido = await _pedidoRepo.UpdateAsync(pedido.Id,pedido);


            if(updatePedido == null)
            {
                response.HasError = true;
                response.Errors.Add("Ocurrio un error al intentar calcular el total del pedido, favor verificar");
                return response;
            }


            response.PrecioUnitario = entity.PrecioUnitario;
            response.CantidadPlatos = entity.CantidadPlatos;
            response.IdPedido = entity.IdPedido;    
            response.IdPlato = entity.IdPlato; 
            return response;    

        }






        #region private method
        public async Task<decimal> CalcularMonto(List<decimal> montos)
        {
            try
            {

                if (montos.Count <= 0)
                {
                    return 0m;
                }


                decimal total = 0;

                foreach (var monto in montos)
                {
                    total += monto;

                }


                return total;

            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al intentar calcular el monto total " + ex.Message);

            }
        }

        #endregion




    }
}
