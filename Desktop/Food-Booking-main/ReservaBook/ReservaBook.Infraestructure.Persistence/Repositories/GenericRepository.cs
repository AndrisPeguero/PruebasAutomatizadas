using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;


namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private readonly ReservaBookContext _appContext;

        public GenericRepository(ReservaBookContext appContext)
        {
        
           this._appContext = appContext;
        
        }


        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {

           await _appContext.Set<TEntity>().AddAsync(entity);
           await _appContext.SaveChangesAsync();
           return entity;     
        }



        public virtual  async  Task<bool> DeleteAsync(int id)
        {

            var Entity = await _appContext.Set<TEntity>().FindAsync(id);
            if (Entity != null)
            {
                 _appContext.Set<TEntity>().Remove(Entity);
                 await _appContext.SaveChangesAsync();
                 return true;
            }

            return false;
        }


       
        public IQueryable<TEntity> GetAllQuariableAsync()
        {
            return _appContext.Set<TEntity>().AsQueryable();
        }



        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            var Entity = await _appContext.Set<TEntity>().FindAsync(id);
            if (Entity != null)
            {
                return Entity;  
            }

            return null;

        }


        public virtual async Task<List<TEntity>> GetlAllAsync()
        {
            return await _appContext.Set<TEntity>().ToListAsync();
        }



        public virtual async Task<TEntity?> UpdateAsync(int id,TEntity entity)
        {
            var EditEntity = await _appContext.Set<TEntity>().FindAsync(id);
            if (EditEntity != null)
            {

                _appContext.Entry(EditEntity).CurrentValues.SetValues(entity);
                await _appContext.SaveChangesAsync();
                return EditEntity;
               
            }

            return null;
            
        }
    }
}
