
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.plato;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;

namespace ReservaBook.Unitt.Test.Services
{
    public class PlatoServiceTest
    {
        private readonly IMapper _mapper;
        private readonly  DbContextOptions<ReservaBookContext> dbContextOptions;


        public PlatoServiceTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                   .UseInMemoryDatabase(databaseName: $"dbImMemoryPlato_{Guid.NewGuid()}")
                                   .Options;



            var loggerFactory = LoggerFactory.Create(cfg =>
            {
                cfg.AddConsole();

            });



            var config = new MapperConfiguration(opt =>
            {
                opt.AddProfile<PlatoEntityToDtosMappingProfile>();
            },loggerFactory);




            _mapper = config.CreateMapper();


        }



        #region private method create services
        public PlatoService CreateService()
        {

            var context = new ReservaBookContext(dbContextOptions);
            var repo = new PlatoRepository(context);
            var service = new PlatoService(repo,_mapper);
            return service;     

        }
        #endregion



        #region testing

        [Fact]
        public async Task AddAsync_should_return_responseDto_when_Added()
        {
           
            //arrange
             var service = CreateService();
            var entity = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            //act
            var result = await service.AddAsync(entity);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Id.Should().BeGreaterThan(0);
            result.Nombre.Should().Be(entity.Nombre);
        }




        [Fact]
        public async Task AddAsync_should_return_null_when_not_Added()
        {

            //arrange
            var service = CreateService();
            CreatePlatoRequestDto entity = null!;
            

            //act
            var result = await service.AddAsync(entity);



            //assert
            result.Should().BeNull();
      
        }


        [Fact]
        public async Task UpdateAsync_should_return_responseDto_when_Updated()
        {

            //arrange
            var service = CreateService();
            var entity = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var entity2 = new CreatePlatoRequestDto()
            {
                Nombre = "Frito con salami",
                Descripcion = "Es un plato de fuerte .....",
                Categoria = PlatoCategoria.PlatosFuertes.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 190.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            //act
            var entityAdded = await service.AddAsync(entity);
            var result = await service.UpdateAsync(entityAdded!.Id,entity2);



            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(entityAdded.Id);
            result.Nombre.Should().Be(entity2.Nombre);  


        }



        [Fact]
        public async Task UpdateAsync_should_return_null_when_not_Updated()
        {

            //arrange
            var service = CreateService();
            var entity = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            CreatePlatoRequestDto updateEntity = null!;
           


            //act
            var entityAdded = await service.AddAsync(entity);
            var result = await service.UpdateAsync(entityAdded!.Id, updateEntity);



            //assert
            result.Should().BeNull();
          
        }




        [Fact]
        public async Task GetAllAsync_should_return_ListResponseDto_when_exist()
        {

            //arrange
            var service = CreateService();
            var listEntities = new List<CreatePlatoRequestDto>();
            var entity = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var entity2 = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            var entity3 = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            listEntities.Add(entity);
            listEntities.Add(entity2);
            listEntities.Add(entity3);
       
            foreach(var _entity in listEntities)
            {

               await service.AddAsync(_entity);

            }


            //act
     
            var result = await service.GetlAllAsync();



            //assert
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(3);
            result.Should().HaveCountGreaterThan(1);

        }





        [Fact]
        public async Task GetAllAsync_should_return_Empty_when_not_exist()
        {

            //arrange
            var service = CreateService();
            
            //act

            var result = await service.GetlAllAsync();



            //assert
            result.Should().BeNullOrEmpty();
        

        }




        [Fact]
        public async Task DeleteAsync_should_return_true_when_deleted()
        {

            //arrange
            var service = CreateService();

            var entity1 = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };



            //act
            var EntityAdded = await service.AddAsync(entity1);
            var result = await service.DeleteAsync(EntityAdded!.Id);



            //assert
            result.Should().BeTrue();


        }


        [Fact]
        public async Task DeleteAsync_should_return_false_when_not_deleted()
        {

            //arrange
            var service = CreateService();

          
            //act
   
            var result = await service.DeleteAsync(999);


            //assert
            result.Should().BeFalse();


        }




        [Fact]
        public async Task GetByIdAsync_should_return_responseDto_when_Exist()
        {

            //arrange
            var service = CreateService();
            var entity1 = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            //act

            var entityAdded = await service.AddAsync(entity1);
            var result = await service.GetByIdAsync(entityAdded!.Id);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(entityAdded!.Id);  
            result.Precio.Should().Be(entityAdded!.Precio); 
         
        }



        [Fact]
        public async Task ChangeStatus_should_return_true_when_changeStatus()
        {


            //arrange
            var service = CreateService();
            var entity = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            //act
            var PlatoAdded = await service.AddAsync(entity);
            var result = await service.ChangeStatus(PlatoAdded!.Id, Estado.NoDisponible.ToString());


            //assert
            result.Should().BeTrue();
      
        }



        [Fact]
        public async Task ChangeStatus_should_return_false_when_not_changeStatus()
        {


            //arrange
            var service = CreateService();
            var entity = new CreatePlatoRequestDto()
            {
                Id = 0,
                Nombre = "Mangu con salami",
                Descripcion = "Es un plato de entrada .....",
                Categoria = PlatoCategoria.Entradas.ToString(),
                Estado = Estado.Disponible.ToString(),
                Precio = 120.25m,
                Imagen = "Images//4d7ad823-3ddb-4ff2-9888-b6b831ff64fa/e668a5df-9f29-4d1c-9591-d3f49ae6d543.jpg"
            };


            //act
            var PlatoAdded = await service.AddAsync(entity);
            var result = await service.ChangeStatus(PlatoAdded!.Id, "");


            //assert
            result.Should().BeFalse();

        }

        #endregion

    }
}
