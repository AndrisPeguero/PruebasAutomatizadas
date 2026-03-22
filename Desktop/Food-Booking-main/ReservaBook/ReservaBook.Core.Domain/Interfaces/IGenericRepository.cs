using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity?>  UpdateAsync(int id,TEntity entity);
        Task<List<TEntity>> GetlAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<TEntity?> GetByIdAsync(int id);
        IQueryable<TEntity> GetAllQuariableAsync();  


    }
}
