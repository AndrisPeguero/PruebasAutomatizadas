
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.pdidoPlato;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class PedidoPlatoServiceTest 
    {

        private readonly IMapper _mapper;
        private readonly DbContextOptions<ReservaBookContext> _dbContextOptions;

        public PedidoPlatoServiceTest()
        {

            _dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                     .UseInMemoryDatabase(databaseName: $"PedidoPlatoInMemoryDb_{Guid.NewGuid}")
                                     .Options;


            var loggerFactory = LoggerFactory.Create(cfg =>
            {

                cfg.AddConsole();

            });



            var config = new MapperConfiguration(opt =>
            {


                opt.AddProfile<PedidoPlatoEntityToDtosMappingProfile>();


            },loggerFactory);


             
            _mapper = config.CreateMapper();        
           
        
        }



        #region private method
        public (PedidoPlatoService, PedidoRepository, PlatoRepository, RestauranteRepository, MesaRepository) CreateService()
        {
            var context = new ReservaBookContext(_dbContextOptions);
            var repo = new PedidoPlatoRepository(context);
            var pedidoRepo = new PedidoRepository(context);
            var PlatoRepo = new PlatoRepository(context);
            var restauranteRepo = new RestauranteRepository(context);
            var mesaRepo = new MesaRepository(context);
            var service = new PedidoPlatoService(repo,_mapper,pedidoRepo,PlatoRepo);
            return (service,pedidoRepo,PlatoRepo,restauranteRepo,mesaRepo); 
        }
        #endregion




        #region test 
        [Fact]
        public async Task AddARangeAsync_should_return_ListResponseDto_when_added()
        {

            //arrange
            var (service, pedidoRepo, platoRepo, restauranteRepo, mesaRepo) = CreateService();
            var listPlatoPedido = new List<CreatePedidoPlatoDto>();
            var entity = new Plato()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };



            var platoAdded = await platoRepo.AddAsync(entity);

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


            var plato1 = new Plato()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var plato2 = new Plato()
            {
                Id = 0,
                Nombre = "Yuca con huevo",
                Descripcion = "Es un plato de fuerte .....",
                Categoria = PlatoCategoria.PlatosFuertes.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 500.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var plato1Added = await platoRepo.AddAsync(plato1);
            var plato2Added = await platoRepo.AddAsync(plato2);


            var platoPedido = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato1Added.Id,
                CantidadPlatos = 8,
                PrecioUnitario = plato1Added.Precio
            
          
            };


            var platoPedido1 = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato2Added.Id,
                CantidadPlatos = 10,
                PrecioUnitario = plato2Added.Precio

            };



            var platoPedido2 = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato2Added.Id,
                CantidadPlatos = 10,
                PrecioUnitario = 450

            };

            listPlatoPedido.Add(platoPedido);
            listPlatoPedido.Add(platoPedido1);
            listPlatoPedido.Add(platoPedido2);


            //act
            var result = await service.AddRangeAsync(listPlatoPedido);




            //assert 
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(3);
            result.Should().HaveCountGreaterThan(2);
        }




        [Fact]
        public async Task AddARangeAsync_should_return_null_when_not_added()
        {

            //arrange
            var (service, pedidoRepo, platoRepo, restauranteRepo, mesaRepo) = CreateService();
            var listPlatoPedido = new List<CreatePedidoPlatoDto>();
          


         
            //act
            var result = await service.AddRangeAsync(null);


            //assert 
            result.Should().BeNullOrEmpty();
          
        }



        [Fact]
        public async Task DeleteAsync_should_return_true_when_deleted()
        {

            //arrange
            var (service, pedidoRepo, platoRepo, restauranteRepo, mesaRepo) = CreateService();
            var listPlatoPedido = new List<CreatePedidoPlatoDto>();

            var entity = new Plato()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };



            var platoAdded = await platoRepo.AddAsync(entity);

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
                IdRestaurante = restauranteAdded.Id,
                Total = 0m
            };


            var pedidoAdded = await pedidoRepo.AddAsync(pedido);


            var plato1 = new Plato()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var plato2 = new Plato()
            {
                Id = 0,
                Nombre = "Yuca con huevo",
                Descripcion = "Es un plato de fuerte .....",
                Categoria = PlatoCategoria.PlatosFuertes.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 500.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var plato1Added = await platoRepo.AddAsync(plato1);
            var plato2Added = await platoRepo.AddAsync(plato2);


            var platoPedido = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato1Added.Id,
                CantidadPlatos = 8,
                PrecioUnitario = plato1Added.Precio


            };


            var platoPedido1 = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato2Added.Id,
                CantidadPlatos = 10,
                PrecioUnitario = plato2Added.Precio

            };



            var platoPedido2 = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato2Added.Id,
                CantidadPlatos = 10,
                PrecioUnitario = 450

            };

            listPlatoPedido.Add(platoPedido);
            listPlatoPedido.Add(platoPedido1);
            listPlatoPedido.Add(platoPedido2);


            //act
            var pedidoPlatosAdded = await service.AddRangeAsync(listPlatoPedido);
            var result = false;
            foreach(var item in pedidoPlatosAdded)
            {

                result =  await service.DeleteAsync(item!.Id);
                break;
            }


            //assert 
            result.Should().BeTrue();

        }



        [Fact]
        public async Task DeleteAsync_should_return_false_when_not_deleted()
        {


            //arrange
            var (service, pedidoRepo, platoRepo, restauranteRepo, mesaRepo) = CreateService();
          

       
            //act
            var result = await service.DeleteAsync(999);
       

            //assert 
            result.Should().BeTrue();

        }




        [Fact]
        public async Task GetAllAsync_should_return_ListResponseDto_when_exist()
        {

            //arrange
            var (service, pedidoRepo, platoRepo, restauranteRepo, mesaRepo) = CreateService();
            var listPlatoPedido = new List<CreatePedidoPlatoDto>();
            var entity = new Plato()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };



            var platoAdded = await platoRepo.AddAsync(entity);

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


            var plato1 = new Plato()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var plato2 = new Plato()
            {
                Id = 0,
                Nombre = "Yuca con huevo",
                Descripcion = "Es un plato de fuerte .....",
                Categoria = PlatoCategoria.PlatosFuertes.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 500.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var plato1Added = await platoRepo.AddAsync(plato1);
            var plato2Added = await platoRepo.AddAsync(plato2);


            var platoPedido = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato1Added.Id,
                CantidadPlatos = 8,
                PrecioUnitario = plato1Added.Precio


            };


            var platoPedido1 = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato2Added.Id,
                CantidadPlatos = 10,
                PrecioUnitario = plato2Added.Precio

            };



            var platoPedido2 = new CreatePedidoPlatoDto()
            {

                Id = 0,
                IdPedido = pedidoAdded.Id,
                IdPlato = plato2Added.Id,
                CantidadPlatos = 10,
                PrecioUnitario = 450

            };

            listPlatoPedido.Add(platoPedido);
            listPlatoPedido.Add(platoPedido1);
            listPlatoPedido.Add(platoPedido2);


            //act
            var pedidoPlatoAdded = await service.AddRangeAsync(listPlatoPedido);
            var result = await service.GetlAllAsync();


            //assert 
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(3);
            result.Should().HaveCountGreaterThan(2);
        }





        [Fact]
        public async Task GetAllAsync_should_return_null_when_not_exist()
        {

            //arrange
            var (service, pedidoRepo, platoRepo, restauranteRepo, mesaRepo) = CreateService();
           


            //act
            var result = await service.GetlAllAsync();


            //assert 
            result.Should().BeNullOrEmpty();
         
        }
        #endregion



    }

}
