

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.pago;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class PagoServiceTest
    {

        private readonly IMapper _mapper;
        private readonly DbContextOptions<ReservaBookContext> dbContextOptions;



        public PagoServiceTest()
        {

            dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                   .UseInMemoryDatabase(databaseName: $"dbInMemoryPago_{Guid.NewGuid}")
                                    .Options;


            var loggerFactory = LoggerFactory.Create(cfg =>
            {

                cfg.AddConsole();

            });



            var config = new MapperConfiguration(opt =>
            {

                opt.AddProfile<PagoEntityToDtosMappingProfile>();

            },loggerFactory);


            _mapper = config.CreateMapper();

        }



        #region private method
        private (PagoService, PedidoRepository, MesaRepository, RestauranteRepository) CreateService()
        {
            var Context = new ReservaBookContext(dbContextOptions);
            var repo = new PagoRepository(Context);
            var pedidoRepo = new PedidoRepository(Context);
            var mesaRepo = new MesaRepository(Context);
            var notificaciones = new NotificacionRepository(Context);
            var restauranteRepo = new RestauranteRepository(Context);
            var service = new PagoService(_mapper,repo,notificaciones,restauranteRepo,pedidoRepo);
            return (service, pedidoRepo,mesaRepo,restauranteRepo);
        
        }
        #endregion





        #region test
        [Fact]
        public async Task AddAsync_should_return_responseDto_when_added()
        {


            //arrange
            var (service, pedidoRepo,mesaRepo,restauranteRepo) = CreateService();

            var restaurante = new Restaurante()
            {
                Id = 0,
                Nombre = "Restaurante Layola",
                Direccion = "Azua, Calle Duartes #91",
                Telefono = "8291239091",
                HorarioInicio = new TimeOnly(08, 30),
                HorarioFin = new TimeOnly(14, 30),
                EspecialidadGastronomica = "Comida Italiana",
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/00c035ee-81e1-44a9-828a-57967eb9b88a.jpg",
                UsuarioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };


            var mesa = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(mesa);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new Pedido()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id
                ,
                Total = 0m
            };


            var pedidoAdded = await pedidoRepo.AddAsync(pedido);


            var pago = new CreatePagoRequesDto()
                      { Id = 0, 
                         Estado = EstadoPago.pendiente.ToString(),
                          Fecha = DateTime.Now,
                          Monto = 500, 
                           UsuarioId = "d1b57480-d8a3-407a-8b38-1d8690c10355",
                            IdPedido = pedidoAdded.Id};

            //act
            var result = await service.AddAsync(pago);  





            //assert
              result.Should().NotBeNull();
              result.Id.Should().Be(1);
              result.Monto.Should().Be(pago.Monto);

        }




        [Fact]
        public async Task AddAsync_should_return_null_when_not__added()
        {


            //arrange
            var (service, pedidoRepo, mesaRepo, restauranteRepo) = CreateService();

           

            //act
            var result = await service.AddAsync(null!);





            //assert
            result.Should().BeNull();
          

        }



        [Fact]
        public async Task GetAllsync_should_return_ListresponseDto_when_exits()
        {


            //arrange
            var (service, pedidoRepo, mesaRepo, restauranteRepo) = CreateService();
            var lisEntities = new List<CreatePagoRequesDto>();
            var restaurante = new Restaurante()
            {
                Id = 0,
                Nombre = "Restaurante Layola",
                Direccion = "Azua, Calle Duartes #91",
                Telefono = "8291239091",
                HorarioInicio = new TimeOnly(08, 30),
                HorarioFin = new TimeOnly(14, 30),
                EspecialidadGastronomica = "Comida Italiana",
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/00c035ee-81e1-44a9-828a-57967eb9b88a.jpg",
                UsuarioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };


            var mesa = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(mesa);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new Pedido()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id
                ,
                Total = 0m
            };


            var pedidoAdded = await pedidoRepo.AddAsync(pedido);


            var pago = new CreatePagoRequesDto()
            {
                Id = 0,
                Estado = EstadoPago.pendiente.ToString(),
                Fecha = DateTime.Now,
                Monto = 100,
                UsuarioId = "d1b57480-d8a3-407a-8b38-1d8690c10355",
                IdPedido = pedidoAdded.Id
            };

            var pago1 = new CreatePagoRequesDto()
            {
                Id = 0,
                Estado = EstadoPago.pendiente.ToString(),
                Fecha = DateTime.Now,
                Monto = 150,
                UsuarioId = "d1b57480-d8a3-407a-8b38-1d8690c10355",
                IdPedido = pedidoAdded.Id
            };


            var pago2 = new CreatePagoRequesDto()
            {
                Id = 0,
                Estado = EstadoPago.pendiente.ToString(),
                Fecha = DateTime.Now,
                Monto = 250,
                UsuarioId = "d1b57480-d8a3-407a-8b38-1d8690c10355",
                IdPedido = pedidoAdded.Id
            };


            lisEntities.Add(pago);
            lisEntities.Add(pago1);
            lisEntities.Add(pago2);

            foreach(var item in lisEntities)
            {
                await service.AddAsync(item);       
            }

            //act
            var result = await service.GetlAllAsync();
            

            //assert
            result.Should().NotBeNull();
            result.Count.Should().Be(3);
            result.Should().HaveCountGreaterThan(2);
           

        }




        [Fact]
        public async Task GetAllsync_should_return_empty_when_exits()
        {


            //arrange
            var (service, pedidoRepo, mesaRepo, restauranteRepo) = CreateService();
           

            //act
            var result = await service.GetlAllAsync();


            //assert
            result.Should().BeNullOrEmpty();
         


        }



        #endregion




    }
}
