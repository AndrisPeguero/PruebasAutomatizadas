

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class RestauranteServiceTest
    {

        private readonly IMapper _mapper;
        private readonly DbContextOptions<ReservaBookContext> dbContextOptions;


        public RestauranteServiceTest()
        {


            dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                              .UseInMemoryDatabase(databaseName: $"InvesmentAppTestDb_{Guid.NewGuid()}")
                              .Options;



            var loggerFactory = LoggerFactory.Create(cf =>
            {

                cf.AddConsole();
            });



            var config = new MapperConfiguration(cfg =>
            {
                //Add your automapper profile here
                cfg.AddProfile<RestauranteEntityToDtoMappingProfile>();
                

            },loggerFactory);
                 


            _mapper = config.CreateMapper();
        
        }


        #region private method
        private RestauranteService CreateService() 
        {
            var context = new ReservaBookContext(dbContextOptions);
            var repo = new RestauranteRepository(context);
            var service = new RestauranteService(repo, _mapper);
            return service;

        }
        #endregion



        #region pruebas 
        [Fact]
        public async Task AddAsync_should_return_ResponseDto_when_Add()
        {

            //arrange
            var service = CreateService();
            var entity = new CreateRestauranteRequestDto()
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
          


            //Act
            var result = await service.AddAsync(entity);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Nombre.Should().Be(entity.Nombre);
            result.UsuarioId.Should().Be(entity.UsuarioId);
           
        }




        [Fact]
        public async Task AddAsync_should_return_null_when_not_Add()
        {

            //arrange
            var service = CreateService();
            CreateRestauranteRequestDto entity = null!;
           


            //Act
            var result = await service.AddAsync(entity);


            //assert
            result.Should().BeNull();
          
          
        }




        [Fact]
        public async Task DeleteAsync_should_return_true_when_deleted()
        {

            //arrange
            var service = CreateService();
            var entity = new CreateRestauranteRequestDto()
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



            //Act
            var addEntity = await service.AddAsync(entity);
            var result = await service.DeleteAsync(addEntity!.Id);



            //assert
            result.Should().BeTrue();

        }




        [Fact]
        public async Task DeleteAsync_should_return_false_when_not_deleted()
        {

            //arrange
            var service = CreateService();
           

            //Act

            var result = await service.DeleteAsync(999);



            //assert
            result.Should().BeFalse();

        }




        [Fact]
        public async Task GetAllAsync_should_return_restaurantes_when_exist()
        {

            //arrange
            var service = CreateService();
            var listEntities = new List<CreateRestauranteRequestDto>();
            var entity1 = new CreateRestauranteRequestDto()
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


            var entity2 = new CreateRestauranteRequestDto()
            {
                Id = 0,
                Nombre = "Restaurante KIO",
                Direccion = "Peralta, Calle Duartes #91",
                Telefono = "8091239091",
                HorarioInicio = new TimeOnly(10, 30),
                HorarioFin = new TimeOnly(18, 30),
                EspecialidadGastronomica = "Comida Francesa",
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/00c035ee-81e1-44a9-828a-57967eb9b88a.jpg",
                UsuarioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"
            };


            var entity3 = new CreateRestauranteRequestDto()
            {
                Id = 0,
                Nombre = "Restaurante Nortwind",
                Direccion = "Peravia, Calle Mella #99",
                Telefono = "8292229091",
                HorarioInicio = new TimeOnly(12, 30),
                HorarioFin = new TimeOnly(21, 30),
                EspecialidadGastronomica = "Comida China",
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/00c035ee-81e1-44a9-828a-57967eb9b88a.jpg",
                UsuarioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"
            };



            listEntities.Add(entity1);
            listEntities.Add(entity2);
            listEntities.Add(entity3);
          
            foreach (var entity in listEntities)
            {
                await service.AddAsync(entity);
            }


            //Act
            var result = await service.GetlAllAsync();



            //assert
            result.Should().NotBeEmpty();
            result.Count.Should().Be(3);
            result[0].Should().NotBeNull();
           
        }



        [Fact]
        public async Task GetAllAsync_should_return_Empty_when_not_exist()
        {

            //arrange
            var service = CreateService();
            
           

           
            //Act
            var result = await service.GetlAllAsync();



            //assert
            result.Should().BeEmpty();
       

        }




        [Fact]
        public async Task GetByIdAsync_should_return_responseDTo_when_Exist()
        {

            //arrange
            var service = CreateService();
            var entity1 = new CreateRestauranteRequestDto()
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


            //Act

            var addEntity = await service.AddAsync(entity1);
            var result = await service.GetByIdAsync(addEntity!.Id);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(addEntity.Id);
            result.Id.Should().BeGreaterThan(0);
           

        }





        [Fact]
        public async Task UpdateAsync_should_return_responseDto_when_Updated()
        {

            //arrange
            var service = CreateService();
            var entity1 = new CreateRestauranteRequestDto()
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


            var entity2 = new CreateRestauranteRequestDto()
            {
                Nombre = "Restaurante Nortwind",
                Direccion = "Peralta, Calle Duartes #99",
                Telefono = "8091238014",
                HorarioInicio = new TimeOnly(14, 30),
                HorarioFin = new TimeOnly(22, 00),
                EspecialidadGastronomica = "Comida China",
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/00c035ee-81e1-44a9-828a-57967eb9b88a.jpg",
                UsuarioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"
            };


            //Act

            var addEntity = await service.AddAsync(entity1);
            var result = await service.UpdateAsync(addEntity!.Id,entity2);



            //assert
            result.Should().NotBeNull();
            result.Nombre.Should().Be(entity2.Nombre);
            result.Direccion.Should().Be(entity2.Direccion);
            result.Id.Should().Be(addEntity.Id);
            result.Id.Should().BeGreaterThan(0);
            

        }

        #endregion





    }
}
