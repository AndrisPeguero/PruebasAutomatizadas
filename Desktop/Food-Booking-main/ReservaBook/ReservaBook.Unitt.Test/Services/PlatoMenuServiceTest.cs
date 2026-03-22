

using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class PlatoMenuServiceTest
    {

        private readonly IMapper _mapper;
        private readonly DbContextOptions<ReservaBookContext> dbContextOptions;



        public PlatoMenuServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                     .UseInMemoryDatabase(databaseName: $"InMemoryDbPlatoMenu_{Guid.NewGuid}")
                                      .Options;



            var loggerFactory = LoggerFactory.Create(cf =>
            {

                cf.AddConsole();
            });



            var config = new MapperConfiguration(cfg =>
            {
                //Add your automapper profile here
                cfg.AddProfile<PlatoMenuEntityToDtosMappingProfile>();


            }, loggerFactory);


            _mapper = config.CreateMapper();

        }




        #region private method
        public (PlatoMenuService,RestauranteRepository) CreateService()
        {
            var context = new ReservaBookContext(dbContextOptions);
            var repo = new PlatoMenuRepository(context);
            var menuRepo = new MenuRepository(context);
            var PlatoRepo = new PlatoRepository(context);
            var restauranteRepo = new RestauranteRepository(context);   
            var service = new PlatoMenuService(repo,menuRepo,PlatoRepo,_mapper);
            return (service,restauranteRepo);
        }
        #endregion




        #region test
        [Fact]
        public async Task AddPlatoAlMenu_should_return_ListResponseDto_when_Added()
        {

            //arrange
            var (service,restauranteRepo) =  CreateService();

            var context = new ReservaBookContext(dbContextOptions);
            var menuRepo = new MenuRepository(context);
            var PlatoRepo = new PlatoRepository(context);

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


            var plato = new Plato()
            {

                Id  = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada muy ...",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.50m,
                Imagen = "https://i.pinimg.com/originals/ae/63/38/ae633823e1c5d0c395263bc05ec4da36.jpg"
            };



            var menu = new Menu()
            {
                Id = 0,
                Nombre = "Comida china",
                Descripcion = "Es un menu orientada a comida china ......",
                IdRestaurante = restauranteAdded.Id


            };

            var PlatoAdded = PlatoRepo.AddAsync(plato);
            var menuAdded = menuRepo.AddAsync(menu);

            List<int> PlatosIdList = new List<int>();
            PlatosIdList.Add(PlatoAdded.Id);

            //act
            var result = await service.AddPlatoAlMenu(menuAdded.Id,PlatosIdList);





            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            result.Should().HaveCountGreaterThan(0);

        }




        [Fact]
        public async Task DeletePlatoDelMenu_should_return_ResponseDto_when_Deleted()
        {

            //arrange
            var (service, restauranteRepo) = CreateService();

            var context = new ReservaBookContext(dbContextOptions);
            var menuRepo = new MenuRepository(context);
            var PlatoRepo = new PlatoRepository(context);

            var plato = new Plato()
            {

                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada muy ...",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.50m,
                Imagen = "https://i.pinimg.com/originals/ae/63/38/ae633823e1c5d0c395263bc05ec4da36.jpg"
            };




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




            var menu = new Menu()
            {
                Id = 0,
                Nombre = "Comida china",
                Descripcion = "Es un menu orientada a comida china ......",
                IdRestaurante = restauranteAdded.Id


            };

            var PlatoAdded = PlatoRepo.AddAsync(plato);
            var menuAdded = menuRepo.AddAsync(menu);

            List<int> PlatosIdList = new List<int>();
            PlatosIdList.Add(PlatoAdded.Id);

            //act
            var PlatoMenuAdded = await service.AddPlatoAlMenu(menuAdded.Id, PlatosIdList);
            var result = await service.DeletePlatoDelMenu(menuAdded.Id,PlatoAdded.Id);



            //assert
            result.Should().NotBeNull();
       
        }
        #endregion


    }
}
