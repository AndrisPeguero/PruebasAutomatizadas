

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;

namespace ReservaBook.Core.Aplication.Services
{
    public class MesaService : GenericService<CreateMesaRequestDto, CreateMesaRequestDto, MesaResponseDto, Mesa>, IMesaService
    {
        private readonly IMapper _mapper;
        private readonly IMesaRepository _mesaRepository;
        private readonly IRestauranteRepository _restauranteRepository;



        public MesaService(IMesaRepository _mesaRepository, IMapper mapper, IRestauranteRepository _restauranteRepository) : base(_mesaRepository, mapper)
        {
            this._mapper = mapper;
            this._mesaRepository = _mesaRepository;
            this._restauranteRepository = _restauranteRepository;

        }





        public override async Task<MesaResponseDto?> AddAsync(CreateMesaRequestDto? entity)
        {

            try
            {
                if (entity == null)
                    return null;


                var response = new MesaResponseDto() { HasError = false, Errors = [] };

                if (entity.CantidadPersonas <= 0)
                {
                    response.HasError = true;
                    response.Errors.Add("El numero de personas debe ser valido, favor verificar");
                    return response;
                }

                var restaurante = await _restauranteRepository.GetByUserId(entity.UsurioId);

                if (restaurante == null)
                {
                    response.HasError = true;
                    response.Errors.Add("No se encontro un restaurante relacionado para esta mesa, debes crear uno si no loas hecho");
                    return response;

                }

                entity.IdRestaurante = restaurante.Id;
                return await base.AddAsync(entity);
            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);

            }

        }

        public async Task<bool> ChangeStatus(int idMesa, string Status)
        {
            try
            {
                if(idMesa <= 0)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(Status))
                {
                    return false;    
                }



                await _mesaRepository.ChangeStatus(idMesa,Status);
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception("Ocurrio un error la intentar cambiar el estado de la mesa "  + ex.Message);
                    
            }
            
        }



        public override async Task<MesaResponseDto?> GetByIdAsync(int id)
        {
            try
            {

                var response = new MesaResponseDto() { HasError = false, Errors = [] };


                var entity = await base.GetByIdAsync(id);

                if (entity == null)
                {
                    response.HasError = true;
                    response.Errors.Add("Message: No se pudo encontrar una mesa con ese id");
                    return response;

                }

                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }




        public override async Task<MesaResponseDto?> UpdateAsync(int id, CreateMesaRequestDto? entity)
        {

            if (entity == null)
            {
                return null;

            }

            var response = new MesaResponseDto() { HasError = true, Errors = [] };

            var IsExit = await _mesaRepository.GetByIdAsync(id);

            if (IsExit == null)
            {
                response.HasError = true;
                response.Errors.Add("No se encontro una mesa con ese id, favor verificar");
                return response;

            }



            entity!.Id = IsExit.Id;
            entity.Estado = IsExit.Estado;
            entity.IdRestaurante = IsExit.IdRestaurante;
            return await base.UpdateAsync(id, entity);

        }





    }
}
