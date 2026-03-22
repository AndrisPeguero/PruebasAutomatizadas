

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Aplication.Dtos.reserva;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class ReservaServiceTest
    {
        private readonly IMapper mapper;
        private readonly DbContextOptions<ReservaBookContext> _dbContextOptions;


        public ReservaServiceTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                    .UseInMemoryDatabase(databaseName: $"ReservaInMemoryDb_{Guid.NewGuid}")
                                    .Options;


            var loggerFactory = LoggerFactory.Create(cfg =>
            {
                 cfg.AddConsole();

            });

            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile<ReservaEntitiyToDtosMappingProfile>();


            },loggerFactory);



            mapper = config.CreateMapper(); 

        }



        #region private method 
        public (ReservaService, MesaRepository, RestauranteRepository) CreateService()
        {
            var context = new ReservaBookContext(_dbContextOptions);
            var repo = new ReservaRepository(context);
            var mesaRepository = new MesaRepository(context);
            var restauranteService = new RestauranteRepository(context);
            var notificacion = new NotificacionRepository(context);
            var service = new ReservaService(repo,mesaRepository,notificacion, restauranteService, mapper);
            return (service,mesaRepository,restauranteService);

        }
        #endregion




        #region test
        [Fact]
        public async Task AddAsync_should_return_responseDto_when_added()
        {


            //arrange
            var (service, mesaRepo, restauranteRepo) = CreateService();
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

             var restauranteAdded =  await  restauranteRepo.AddAsync(restaurante);
        

            var entity = new Mesa()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString()
            
            };

            var mesaAdded = await mesaRepo.AddAsync(entity);



            var reserva = new CreateReservaRequestDto()
            {

                Id = 0,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Estado = EstadoSolicitudes.Pendiente.ToString(),
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                Hora = new TimeOnly(12,40),     
                IdUsuario = "d1b57480-d8a3-407a-8b38-1d8690c10355"

            };


            //act
            var result = await service.AddAsync(reserva);   



            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1); 
           
        }





        [Fact]
        public async Task AddAsync_should_return_null_when_not_added()
        {


            //arrange
            var (service, mesaRepo, restauranteRepo) = CreateService();



            CreateReservaRequestDto reserva = null!;
          


            //act
            var result = await service.AddAsync(reserva);



            //assert
            result.Should().BeNull();
          

        }



        [Fact]
        public async Task UpdateAsync_should_return_responseDto_when_added()
        {


            //arrange
            var (service, mesaRepo, restauranteRepo) = CreateService();
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
                Estado = Estado.Disponible.ToString()

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);



            var reserva = new CreateReservaRequestDto()
            {

                Id = 0,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Estado = EstadoSolicitudes.Pendiente.ToString(),
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                Hora = new TimeOnly(12, 40),
                IdUsuario = "d1b57480-d8a3-407a-8b38-1d8690c10355"

            };


            var reserva2 = new CreateReservaRequestDto()
            {

                Id = 0,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Estado = EstadoSolicitudes.Pendiente.ToString(),
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(25)),
                Hora = new TimeOnly(22, 30),
                IdUsuario = "d1b57480-d8a3-407a-8b38-1d8690c10355"

            };


            //act
            var reservaAdded = await service.AddAsync(reserva);
            var result = await service.UpdateAsync(reservaAdded!.Id,reserva2);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Fecha.Should().Be(reserva2.Fecha);
            result.Hora.Should().Be(reserva2.Hora);     

        }




        [Fact]
        public async Task UpdateAsync_should_return_null_when_not_added()
        {


            //arrange
            var (service, mesaRepo, restauranteRepo) = CreateService();
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
                Estado = Estado.Disponible.ToString()

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);



            var reserva = new CreateReservaRequestDto()
            {

                Id = 0,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Estado = EstadoSolicitudes.Pendiente.ToString(),
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                Hora = new TimeOnly(12, 40),
                IdUsuario = "d1b57480-d8a3-407a-8b38-1d8690c10355"

            };


            CreateReservaRequestDto reserva2 = null!;
           


            //act
            var reservaAdded = await service.AddAsync(reserva);
            var result = await service.UpdateAsync(reservaAdded!.Id, reserva2);



            //assert
            result.Should().BeNull();
         

        }



        [Fact]
        public async Task DeleteAsync_should_return_true_when_deleted()
        {


            //arrange
            var (service, mesaRepo, restauranteRepo) = CreateService();
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
                Estado = Estado.Disponible.ToString()

            };

            var mesaAdded = await mesaRepo.AddAsync(entity);



            var reserva = new CreateReservaRequestDto()
            {

                Id = 0,
                IdMesa = mesaAdded.Id,
                IdRestaurante = restauranteAdded.Id,
                Estado = EstadoSolicitudes.Pendiente.ToString(),
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                Hora = new TimeOnly(12, 40),
                IdUsuario = "d1b57480-d8a3-407a-8b38-1d8690c10355"

            };


          



            //act
            var reservaAdded = await service.AddAsync(reserva);
            var result = await service.DeleteAsync(reservaAdded!.Id);



            //assert
            result.Should().BeTrue();


        }




        [Fact]
        public async Task DeleteAsync_should_return_false_when_deleted()
        {


            //arrange
            var (service, mesaRepo, restauranteRepo) = CreateService();
           

            //act
         
            var result = await service.DeleteAsync(999);



            //assert
            result.Should().BeFalse();


        }

        #endregion
        
    }
}
