
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.Reseña;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class ReseñaServiceTest
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions<ReservaBookContext> _dbContextOptions;


        public ReseñaServiceTest()
        {

            _dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                      .UseInMemoryDatabase(databaseName: $"ReseniaInMemoryDb_{Guid.NewGuid}")
                                       .Options;

            var loggerFactory = LoggerFactory.Create(cfg =>
            {

                cfg.AddConsole();

            });



            var config = new MapperConfiguration(opt =>
            {


                opt.AddProfile<ReseñaEntityToDtoMappingProfile>();

            },loggerFactory);



            _mapper = config.CreateMapper();   
        }


        #region private method
        public (ReseñaService, RestauranteRepository) CreateService()
        {
            var context = new ReservaBookContext(_dbContextOptions);
            var repo = new ReseñaRepository(context);
            var notificacion = new NotificacionRepository(context);
            var restauranteRepo = new RestauranteRepository(context);   
            var service = new ReseñaService(repo,notificacion,restauranteRepo,_mapper);
            return (service,restauranteRepo); 
        }
        #endregion



        #region test
        [Fact]
        public async Task AddAsync_should_return_responseDto_when_added()
        {

            //arrange
            var (service,restauranteRepo) = CreateService();
            var entity = new Restaurante()
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



            var restauranteAdded = await restauranteRepo.AddAsync(entity);

            var resenia = new CreateReseñaDto()
                { Id = 0,
                 IdRestaurante = restauranteAdded.Id,
                 CantidadEstrella = 5,
                 Descripcion = "me gusto mucho la comida y el servicio al cliente es excelente"};


            //act
            var result = await service.AddAsync(resenia);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Descripcion.Should().Be(resenia.Descripcion);

        }



        [Fact]
        public async Task AddAsync_should_return_null_when_not_added()
        {


            //arrange
            var (service, restauranteRepo) = CreateService();


            CreateReseñaDto resenia = null!;
            

            //act
            var result = await service.AddAsync(resenia);


            //assert
            result.Should().BeNull();
      

        }



        [Fact]
        public async Task UpdateAsync_should_return_responseDto_when_updated()
        {


            //arrange
            var (service, restauranteRepo) = CreateService();
            var entity = new Restaurante()
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



            var restauranteAdded = await restauranteRepo.AddAsync(entity);

            var resenia = new CreateReseñaDto()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                CantidadEstrella = 5,
                Descripcion = "me gusto mucho la comida y el servicio al cliente es excelente"
            };


            var resenia2 = new CreateReseñaDto()
            {
               
                CantidadEstrella = 3,
                Descripcion = "me gusto mucho la comida,pero el servicio al cliente estuvo en parte muy mal"
            };




            //act
            var reseniaAdded = await service.AddAsync(resenia);
            var result = await service.UpdateAsync(reseniaAdded!.Id,resenia2);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Descripcion.Should().Be(resenia2.Descripcion);


        }





        [Fact]
        public async Task UpdateAsync_should_return_null_when_not_updated()
        {


            //arrange
            var (service, restauranteRepo) = CreateService();
            var entity = new Restaurante()
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



            var restauranteAdded = await restauranteRepo.AddAsync(entity);

            var resenia = new CreateReseñaDto()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                CantidadEstrella = 5,
                Descripcion = "me gusto mucho la comida y el servicio al cliente es excelente"
            };


           



            //act
            var reseniaAdded = await service.AddAsync(resenia);
            var result = await service.UpdateAsync(reseniaAdded!.Id, null);


            //assert
            result.Should().BeNull();
          
        }



        [Fact]
        public async Task DeleteAsync_should_return_true_when_deleted()
        {


            //arrange
            var (service, restauranteRepo) = CreateService();
            var entity = new Restaurante()
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



            var restauranteAdded = await restauranteRepo.AddAsync(entity);

            var resenia = new CreateReseñaDto()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                CantidadEstrella = 5,
                Descripcion = "me gusto mucho la comida y el servicio al cliente es excelente"
            };






            //act
            var reseniaAdded = await service.AddAsync(resenia);
            var result = await service.DeleteAsync(reseniaAdded!.Id);


            //assert
            result.Should().BeTrue();

        }






        [Fact]
        public async Task DeleteAsync_should_return_false_when_not_deleted()
        {


            //arrange
            var (service, restauranteRepo) = CreateService();
          


            //act
       
            var result = await service.DeleteAsync(999);


            //assert
            result.Should().BeFalse();

        }


        [Fact]
        public async Task GetAllByIdRestauranteAsync_should_return_ListresponseDto_when_exists()
        {

            //arrange
            var (service, restauranteRepo) = CreateService();
            var listEntities = new List<CreateReseñaDto>();
            var entity = new Restaurante()
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



            var restauranteAdded = await restauranteRepo.AddAsync(entity);

            var resenia = new CreateReseñaDto()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                CantidadEstrella = 5,
                Descripcion = "me gusto mucho la comida y el servicio al cliente es excelente"
            };

            var resenia2 = new CreateReseñaDto()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                CantidadEstrella = 2,
                Descripcion = "me gusto mucho la comida, pero  el servicio al cliente estuvo mal"
            };

            var resenia3 = new CreateReseñaDto()
            {
                Id = 0,
                IdRestaurante = restauranteAdded.Id,
                CantidadEstrella = 2,
                Descripcion = "la comida estava regular y el no me gusto el servicio al cliente"
            };

            listEntities.Add(resenia);
            listEntities.Add(resenia2);
            listEntities.Add(resenia3);

            foreach(var item in listEntities)
            {
                await service.AddAsync(item);
            }

            //act
            var result = await service.GetAllByIdRestaurnteAsync(restauranteAdded.Id);


            //assert
            result.Should().NotBeNull();
            result.Count.Should().Be(3);
            result.Should().HaveCountGreaterThan(2);

        }


        #endregion

    }
}
