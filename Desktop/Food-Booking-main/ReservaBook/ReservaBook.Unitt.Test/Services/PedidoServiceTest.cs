

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class PedidoServiceTest
    {

        private readonly IMapper _mapper;
        private readonly DbContextOptions<ReservaBookContext> _dbContextOptions;



        public PedidoServiceTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                   .UseInMemoryDatabase(databaseName: $"PedidoDbInMemory_{Guid.NewGuid}")
                                   .Options;



            var loggerFactory = LoggerFactory.Create(cfg =>
            {

                cfg.AddConsole();

            });




            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile<PedidoEntityTODtosMappingProfile>();

            },loggerFactory);
        

            _mapper = config.CreateMapper();
        
        }




        #region private method
        public PedidoService CreateService()
        {
            var context = new ReservaBookContext(_dbContextOptions);
            var repo = new PedidoRepository(context);
            var mesaRepo = new MesaRepository(context);
            var notificaciones = new NotificacionRepository(context);
            var restauranteRepo = new RestauranteRepository(context);
            var service = new PedidoService(repo,mesaRepo,restauranteRepo,notificaciones,_mapper);
            return service;
        }
        #endregion




        #region test
        [Fact]
        public async Task AddAsync_Should_return_responseDto_when_added()
        {


            //arrange
            var service  = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


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

        
            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
               
            };

            var mesaAdded = await mesaRepo.AddAsync(entity);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id
                ,Total = 0m
            };

       


            //act
            var result = await service.AddAsync(pedido);





            //assert
            result.Should().NotBeNull();
            result.IdMesa.Should().Be(mesaAdded.Id);
            result.IdRestaurante.Should().Be(restauranteAdded.Id);
            result.Id.Should().BeGreaterThan(0);

        }




        [Fact]
        public async Task AddAsync_Should_return_null_when_not_added()
        {


            //arrange
            var service = CreateService();


            CreatePedidoRequestDto pedido = null!;
           



            //act
            var result = await service.AddAsync(pedido);



            //assert
            result.Should().BeNull();
        
        }





        [Fact]
        public async Task UpdateAsync_Should_return_responseDto_when_updated()
        {


            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


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


            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m,
            };

            var pedidoAdded = await service.AddAsync(pedido);

            var Editpedido = new CreatePedidoRequestDto()
            {

                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                Hora = TimeOnly.FromTimeSpan(TimeSpan.FromHours(10)),
                Estado = pedidoAdded!.Estado,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m,
            };

            //act
            var result = await service.UpdateAsync(pedidoAdded.Id, Editpedido); 


            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(pedidoAdded.Id);
            result.IdMesa.Should().Be(mesaAdded.Id);
            result.IdRestaurante.Should().Be(pedidoAdded.Id);

        }






        [Fact]
        public async Task UpdateAsync_Should_return_null_when_not_updated()
        {


            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


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



            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m,
            };

            var pedidoAdded = await service.AddAsync(pedido);

            CreatePedidoRequestDto editPedido = null!;
          

            //act
            var result = await service.UpdateAsync(pedidoAdded!.Id, editPedido);


            //assert
            result.Should().BeNull();
          
        }




        [Fact]
        public async Task DeleteAsync_Should_return_true_when_deleted()
        {


            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


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



            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m,
            };

            var pedidoAdded = await service.AddAsync(pedido);

    
            //act
            var result = await service.DeleteAsync(pedidoAdded!.Id);


            //assert
            result.Should().BeTrue();

        }





        [Fact]
        public async Task DeleteAsync_Should_return_false_when_not_deleted()
        {


            //arrange
            var service = CreateService();
           

            //act
            var result = await service.DeleteAsync(999);


            //assert
            result.Should().BeFalse();

        }


        [Fact]
        public async Task GetAllAsync_Should_return_ListResponseDto_when_exist()
        {



            //arrange
          
            var listEntities = new List<CreatePedidoRequestDto>();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var repo = new PedidoRepository(context);
            var notificacion = new NotificacionRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


            var service = new PedidoService(repo,mesaRepo,restauranteRepo,notificacion,_mapper);

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

            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);
         

            var pedido = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = new TimeOnly(19,2),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m,
            };


            var pedido1 = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                Hora = new TimeOnly(20,0),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id
                ,Total = 0,
            };



            var pedido2 = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                Hora = new TimeOnly(22,0),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m,
            };


            listEntities.Add(pedido);
            listEntities.Add(pedido1);
            listEntities.Add(pedido2);

            //act

            foreach(var _pedido in listEntities)
            {

                await service.AddAsync(_pedido);

            }
         

            var result = await service.GetlAllAsync();  



            //assert

            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);
       
      
        }





        [Fact]
        public async Task GetAllAsync_Should_return_empty_when_not_exist()
        {


            //arrange
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var repo = new PedidoRepository(context);
            var notificacion = new NotificacionRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


            var service = new PedidoService(repo, mesaRepo, restauranteRepo, notificacion,_mapper);


            //act
            var result = await service.GetlAllAsync();



            //assert
            result.Should().BeNullOrEmpty();
      


        }




        [Fact]
        public async Task GetByIdAsync_Should_return_responseDto_when__exist()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


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



            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);
            var restauranteAdded = await restauranteRepo.AddAsync(restaurante);

            var pedido = new CreatePedidoRequestDto()
            {

                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                Hora = TimeOnly.FromDateTime(DateTime.UtcNow),
                Estado = EstadoPedido.Pendiente,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Total = 0m
            };

            var pedidoAdded = await service.AddAsync(pedido);




            //act
            var result = await service.GetByIdAsync(pedidoAdded!.Id);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(pedidoAdded.Id);  

        }






        [Fact]
        public async Task GetByIdAsync_Should_return_null_when_not_exist()
        {



            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var mesaRepo = new MesaRepository(context);
            var restauranteRepo = new RestauranteRepository(context);


           
            //act
            var result = await service.GetByIdAsync(999);



            //assert
            result.Should().BeNull();
           

        }


        #endregion



    }
}
