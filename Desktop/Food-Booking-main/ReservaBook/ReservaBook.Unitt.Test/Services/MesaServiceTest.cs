using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Mappings.EntitiesToDto;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Unitt.Test.Services
{
    public class MesaServiceTest
    {
        private readonly DbContextOptions<ReservaBookContext> _dbContextOptions;
        private readonly IMapper _mapper;



        public MesaServiceTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ReservaBookContext>()
                                .UseInMemoryDatabase(databaseName: $"InMemoryDb210_{Guid.NewGuid()}")
                                .Options;


            var loggerFactory = LoggerFactory.Create(cfg =>
            {

                cfg.AddConsole();

            });


            var config = new MapperConfiguration(opt =>
            {

                opt.AddProfile<MesaEntityToDtoMappingProfile>();   

            },loggerFactory);
        
        
        
            _mapper = config.CreateMapper();    
        
        }




        #region private method
        public MesaService CreateService() 
        {
            var context = new ReservaBookContext(_dbContextOptions);
            var repo = new MesaRepository(context);
            var repoRestaurante = new RestauranteRepository(context);   
            var service = new MesaService(repo, _mapper,repoRestaurante);
            return service;
       
        }
        #endregion


        #region test 

        [Fact]
        public async Task AddAsync_Should_return_responseDto_when_added()
        {
        
            //arrange
             var service = CreateService();
             var context = new ReservaBookContext(_dbContextOptions);


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

             context.Restaurantes.Add(restaurante);
             await context.SaveChangesAsync();  



            var entity = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };



            //act
            var result = await service.AddAsync(entity);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Nombre.Should().Be(entity.Nombre);
        
        
        }




        [Fact]
        public async Task AddAsync_Should_return_null_when_not_added()
        {

            //arrange
            var service = CreateService();
           
            CreateMesaRequestDto entity = null!;


            //act
            var result = await service.AddAsync(entity);


            //assert
            result.Should().BeNull();
         
        }


        [Fact]
        public async Task UpdateAsync_Should_return_responseDto_when_Updated()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);


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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();



            var entity1 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };


            var entitiy1Added = await service.AddAsync(entity1);

            var entity2 = new CreateMesaRequestDto()
            {

                Id = entitiy1Added!.Id,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };





            //act
            var result = await service.UpdateAsync(entitiy1Added.Id,entity2);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Nombre.Should().Be(entity2.Nombre);
            result.Descripcion.Should().Be(entity2.Descripcion);


        }




        [Fact]
        public async Task UpdateAsync_Should_return_null_when_not_Updated()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);


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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();



            var entity1 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };


            var entitiy1Added = await service.AddAsync(entity1);

            CreateMesaRequestDto entity = null!;
            

            //act
            var result = await service.UpdateAsync(entitiy1Added.Id, entity);


            //assert
            result.Should().BeNull();
       
        }




        [Fact]
        public async Task DeleteAsync_Should_true__when_Deleted()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);


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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();



            var entity1 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };


            var entitiy1Added = await service.AddAsync(entity1);
            


            //act
            var result = await service.DeleteAsync(entitiy1Added!.Id);


            //assert
            result.Should().BeTrue();

        }






        [Fact]
        public async Task DeleteAsync_Should_false_when_not_Deleted()
        {

            //arrange
            var service = CreateService();
           



            //act
            var result = await service.DeleteAsync(999);


            //assert
            result.Should().BeFalse();

        }




        [Fact]
        public async Task GetAllAsync_Should_return_ListResponseDto_when_Exist()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var listEntities = new List<CreateMesaRequestDto>();

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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();



            var entity1 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };



            var entity2 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa Fuego",
                Descripcion = "Es un es una mesa fuego ....",
                CantidadPersonas = 2,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };



            var entity3 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa Ballena",
                Descripcion = "Es un es una mesa Ballena ....",
                CantidadPersonas = 8,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };

            listEntities.Add(entity1);  
            listEntities.Add(entity2);
            listEntities.Add(entity3);

            foreach(var entity in listEntities)
            {

                var entitiy1Added = await service.AddAsync(entity);

            }
       

            //act
            var result = await service.GetlAllAsync();


            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);

        }







        [Fact]
        public async Task GetAllAsync_Should_return_null_when_not_Exist()
        {

            //arrange
            var service = CreateService();
           

            //act
            var result = await service.GetlAllAsync();


            //assert
            result.Should().BeNullOrEmpty();
            result.Should().HaveCount(0);

        }





        [Fact]
        public async Task GetByIdAsync_Should_return_ResponseDto_when_Exist()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);
            var listEntities = new List<CreateMesaRequestDto>();

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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();


            var entity1 = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa Ballena",
                Descripcion = "Es un es una mesa Ballena ....",
                CantidadPersonas = 8,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };


            var entitiAdded = await service.AddAsync(entity1);


            //act
            var result = await service.GetByIdAsync(entitiAdded.Id);


            //assert
            result.Should().NotBeNull();
            result.Id.Should().Be(entitiAdded.Id);
            result.Id.Should().BeGreaterThan(0);

        }





        [Fact]
        public async Task ChangeStatus_Should_return_true_when_Change()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);


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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();



            var entity = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };




            //act
            var MesaAdded = await service.AddAsync(entity);
            var result = await service.ChangeStatus(MesaAdded!.Id,Estado.NoDisponible.ToString());


            //assert
            result.Should().BeTrue();
          


        }




        [Fact]
        public async Task ChangeStatus_Should_return_false_when_not_Change()
        {

            //arrange
            var service = CreateService();
            var context = new ReservaBookContext(_dbContextOptions);


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

            context.Restaurantes.Add(restaurante);
            await context.SaveChangesAsync();



            var entity = new CreateMesaRequestDto()
            {
                Id = 0,
                IdRestaurante = restaurante.Id,
                Nombre = "Mesa tornado",
                Descripcion = "Es un es una mesa tornado ....",
                CantidadPersonas = 5,
                Estado = Estado.Disponible.ToString(),
                UsurioId = "4d7ad823-3ddb-4ff2-9888-b6b831ff64fa"

            };




            //act
            var MesaAdded = await service.AddAsync(entity);
            var result = await service.ChangeStatus(MesaAdded!.Id, " ");


            //assert
            result.Should().BeFalse();

        }


        #endregion



    }
}
