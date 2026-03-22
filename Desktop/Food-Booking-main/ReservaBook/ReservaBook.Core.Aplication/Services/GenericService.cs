using AutoMapper;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Interfaces;


namespace ReservaBook.Core.Aplication.Services
{
    public class GenericService<ModelDtoAdd, ModelDtoUpdate, TResponse, Entity> : IGenericService<ModelDtoAdd, ModelDtoUpdate, TResponse,Entity>
      where ModelDtoAdd : class where ModelDtoUpdate : class where Entity : class where TResponse : class
    {

        private readonly IGenericRepository<Entity> _genericRepo;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> genericRepository, IMapper _mapper)
        {

            this._mapper = _mapper;
            _genericRepo = genericRepository;

        }



        public virtual async Task<TResponse?> AddAsync(ModelDtoAdd? entity)
        {

            if (entity == null)
            {

                return null;
                
            }

            var newEntity =  _mapper.Map<Entity>(entity);
             
             
            var returnEntity = await _genericRepo.AddAsync(newEntity);


            var response = _mapper.Map<TResponse>(returnEntity);
            return response;

        }





        public virtual async Task<bool> DeleteAsync(int id)
        {


            if (id <= 0)
            {
                return false;
            }


            return await _genericRepo.DeleteAsync(id);

        }





        public virtual async Task<TResponse?> GetByIdAsync(int id)
        {

            if (id <= 0)
            {
                return null;
            }

            var entity = await _genericRepo.GetByIdAsync(id);
            return _mapper.Map<TResponse>(entity);

        }




        public virtual async Task<List<ModelDtoAdd?>> GetlAllAsync()
        {


            var listEntity = await _genericRepo.GetlAllAsync();

            return _mapper.Map<List<ModelDtoAdd>>(listEntity)!;


        }




        public virtual async Task<TResponse?> UpdateAsync(int id, ModelDtoUpdate? entity)
        {


          
            if (id <= 0)
            {
                return null;

            }


            var UpdatedEntity = _mapper.Map<Entity>(entity);

            var entityUpdated = await _genericRepo.UpdateAsync(id, UpdatedEntity);


            var dto = _mapper.Map<TResponse>(entityUpdated);
            return dto;

        }

    }
}
