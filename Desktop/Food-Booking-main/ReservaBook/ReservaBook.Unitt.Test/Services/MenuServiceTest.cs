

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.menu;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;
using System.Runtime.CompilerServices;

namespace ReservaBook.Unitt.Test.Services
{
    public class MenuServiceTest
    {

        private readonly IMapper mapper;
        private readonly DbContextOptions<ReservaBookContext> _dbContextOptions;




        public MenuServiceTest()
        {

            _dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                    .UseInMemoryDatabase(databaseName: $"dbInMemoryMenu_{Guid.NewGuid()}")
                                    .Options;


            var loggerFactory = LoggerFactory.Create(cfg =>
            {


                cfg.AddConsole();

            });


            var config = new MapperConfiguration(opt =>
            {

                opt.AddProfile<MenuEntityToDtoMappingProfile>();



            },loggerFactory);

            mapper = config.CreateMapper();

        }



        #region private method
        public (MenuService, RestauranteRepository) CreateService()
        {

            var context = new ReservaBookContext(_dbContextOptions);
            var repo = new MenuRepository(context);
            var RestauranteRepo = new RestauranteRepository(context);
            var service = new MenuService(repo,mapper, RestauranteRepo);
            return (service,RestauranteRepo);

        }
        #endregion




        #region test

        [Fact]
        public async Task AddAysnc_Should_return_responseDto_when_added()
        {

            //arrage
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

            var menu = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu picante",
                Descripcion = "Es un menu orientado a comida picante",
                IdRestaurante = restauranteAdded.Id


            };




            //act
            var result = await service.AddAsync(menu);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Nombre.Should().Be(menu.Nombre);
           
        }





        [Fact]
        public async Task AddAysnc_Should_return_null_when_not_added()
        {

            //arrage
            var (service,restauranteRepo) = CreateService();
            CreateMenuDto menu = null!;
            




            //act
            var result = await service.AddAsync(menu);



            //assert
            result.Should().BeNull();
       
        }




        [Fact]
        public async Task UpdateAsync_Should_return_responseDto_when_Updated()
        {

            //arrage
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


            var menu = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu picante",
                Descripcion = "Es un menu orientado a comida picante",
                IdRestaurante = restauranteAdded.Id,

            };



            var menu2 = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu Inglesa",
                Descripcion = "Es un menu orientado a comida Inglesa",
                IdRestaurante = restauranteAdded.Id,

            };





            //act
            var menuAddes = await service.AddAsync(menu);
            var result = await service.UpdateAsync(menuAddes!.Id,menu2);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Id.Should().Be(menuAddes.Id);
            result.Nombre.Should().Be(menu2.Nombre); 

        }



        [Fact]
        public async Task UpdateAsync_Should_return_null_when_not_Updated()
        {

            //arrage
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



            var menu = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu picante",
                Descripcion = "Es un menu orientado a comida picante",
                IdRestaurante = restauranteAdded.Id,
            };




            CreateMenuDto menu2 = null!;
           





            //act
            var menuAddes = await service.AddAsync(menu);
            var result = await service.UpdateAsync(menuAddes!.Id, menu2);



            //assert
            result.Should().BeNull();
         

        }


        [Fact]
        public async Task DeleteAsync_Should_return_true_when_Deleted()
        {

            //arrage
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


            var menu = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu picante",
                Descripcion = "Es un menu orientado a comida picante",
                IdRestaurante = restauranteAdded.Id,
                
            };



        
            //act
            var menuAddes = await service.AddAsync(menu);
            var result = await service.DeleteAsync(menuAddes!.Id);



            //assert
            result.Should().BeTrue();


        }



        [Fact]
        public async Task DeleteAsync_Should_return_false_when_not_Deleted()
        {

            //arrage
            var (service,restauranteRepo) = CreateService();
          





            //act
    
            var result = await service.DeleteAsync(999);



            //assert
            result.Should().BeFalse();


        }



        [Fact]
        public async Task GetAllAsync_Should_return_ListReponseDto_when_exist()
        {


            //arrage
            var (service, restauranteRepo) = CreateService();
            var listEntities = new List<CreateMenuDto>();
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


            var menu = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu picante",
                Descripcion = "Es un menu orientado a comida picante",
                IdRestaurante = restauranteAdded.Id

            };


            var menu2 = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu ingles",
                Descripcion = "Es un menu orientado a comida inglesa",
                IdRestaurante = restauranteAdded.Id

            };


            var menu3 = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu chino",
                Descripcion = "Es un menu orientado a comida china",
                IdRestaurante = restauranteAdded.Id

            };



            listEntities.Add(menu);
            listEntities.Add(menu2);
            listEntities.Add(menu3);

            foreach(var _entity in listEntities)
            {
                await service.AddAsync(_entity);
            }


            //act
            var result = await service.GetlAllAsync();      

       



            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);   
            result.Count.Should().BeGreaterThan(1);    


        }




        [Fact]
        public async Task GetAllAsync_Should_return_Empty_when_not_exist()
        {


            //arrage
            var (service, restauranteRepo) = CreateService();

           

            //act
            var result = await service.GetlAllAsync();





            //assert
            result.Should().BeNullOrEmpty();
           
        }



        [Fact]
        public async Task GetByIdAsync_Should_return_responseDto_when_finded()
        {


            //arrage
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

            var menu = new CreateMenuDto()
            {

                Id = 0,
                Nombre = "Menu chino",
                Descripcion = "Es un menu orientado a comida china",
                IdRestaurante = restauranteAdded.Id

            };



            //act
            var entitieAdded = await service.AddAsync(menu);
            var result = await service.GetByIdAsync(entitieAdded!.Id);

            



            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Id.Should().Be(entitieAdded.Id);
            result.Nombre.Should().Be(entitieAdded.Nombre);

        }

        #endregion



    }
}
