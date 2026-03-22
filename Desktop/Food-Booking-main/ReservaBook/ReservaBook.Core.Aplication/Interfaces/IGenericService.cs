

using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IGenericService<AddEntityDto,UpdateEntityDto,TResponse, Entity> 
   where AddEntityDto  : class where UpdateEntityDto : class where TResponse : class where Entity : class
    {

        Task<TResponse?> AddAsync(AddEntityDto? entity);
        Task<TResponse?> UpdateAsync(int id, UpdateEntityDto? entity);
        Task<List<AddEntityDto?>> GetlAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<TResponse?> GetByIdAsync(int id);
   

    }
}
