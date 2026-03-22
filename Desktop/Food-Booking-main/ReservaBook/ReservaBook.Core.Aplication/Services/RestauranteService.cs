

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;

namespace ReservaBook.Core.Aplication.Services
{
    public class RestauranteService : GenericService<CreateRestauranteRequestDto, CreateRestauranteRequestDto, RestauranteResponseDto, Restaurante>, IRestauranteServices
    {
        private readonly IRestauranteRepository _repository;



        public RestauranteService(IRestauranteRepository _repository, IMapper _mapper) : base(_repository, _mapper)
        {
            this._repository = _repository;
        }



        public override async Task<RestauranteResponseDto?> AddAsync(CreateRestauranteRequestDto? entity)
        {

            try  
            {
                if (entity == null)
                    return null;


                var response = new RestauranteResponseDto() { HasError = false, Errors = [], IsCreated = false};

                if (entity.Telefono.Length > 10 || entity.Telefono.Length < 10)
                {
                    response.HasError = true;
                    response.Errors.Add("El numero de telefono debe contener 10 digitos");
                    return response;
                }


                if (entity.HorarioInicio >= entity.HorarioFin)
                {
                    response.HasError = true;   
                    response.Errors.Add("El horario de inicio no puede ser mayor que el horario final");
                    return response;
                }


                response.IsCreated = true;
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            
            }
           
        }



        public override async Task<RestauranteResponseDto?> GetByIdAsync(int id)
        {
            try
            {

                var response = new RestauranteResponseDto() { HasError = false, Errors = [] };


                var entity = await base.GetByIdAsync(id);

                if (entity == null)
                {
                    response.HasError = true;
                    response.Errors.Add("Message: No se pudo encontrar un restaurante con ese id");
                    return response;

                }

                return entity;
            }
            catch (Exception ex)
            {
           
                throw new Exception(ex.Message);
            
            }
        }




       







        public override async Task<RestauranteResponseDto?> UpdateAsync(int id, CreateRestauranteRequestDto? entity)
        {
                 var response = new RestauranteResponseDto() { HasError = false, Errors = [] };

            try
            {

                var IsExist = await _repository.GetByIdAsync(id);


                if (IsExist == null)
                {
                    response.HasError = true;
                    response.Errors.Add("No se contro un restaurante con ese id");
                    return response;
                }


                var UpdateEntity = new CreateRestauranteRequestDto()
                {
                    Id = IsExist.Id,
                    Nombre = string.IsNullOrWhiteSpace(entity!.Nombre) ? IsExist.Nombre : entity.Nombre,
                    Direccion = string.IsNullOrWhiteSpace(entity.Direccion) ? IsExist.Direccion : entity.Direccion,
                    EspecialidadGastronomica = string.IsNullOrWhiteSpace(entity.EspecialidadGastronomica) ? IsExist.EspecialidadGastronomica : entity.EspecialidadGastronomica,
                    HorarioFin = entity.HorarioFin,
                    HorarioInicio = entity.HorarioInicio,
                    Telefono = string.IsNullOrWhiteSpace(entity.Telefono) ? IsExist.Telefono : entity.Telefono,
                    UsuarioId = entity.UsuarioId,
                    Imagen = string.IsNullOrWhiteSpace(entity.Imagen) ? IsExist.Imagen : entity.Imagen
                    
                }; 


                var returnEntity = await base.UpdateAsync(id, UpdateEntity);

                return returnEntity;

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            
            
            }

        }


        public async  Task<DeleteRestauranteResponseDto?> DeleteRestauranteAsync(int id)
        {
            var response = new DeleteRestauranteResponseDto() { Success = false, IsCreated = true};

            try
            {

                bool result = await base.DeleteAsync(id);

                if (result)
                {
                    response.Success = true;
                    response.IsCreated = true;
                    return response;
                }

                return response;

            }
            catch (Exception ex)
            {

                 
                throw new Exception("Ocurrio un error al intentar eliminar el restaurante " + ex.Message);
            
            
            }
        }
    }



}

