

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IRestauranteRepository : IGenericRepository<Restaurante>
    {


        Task<Restaurante?> GetByUserId(string UserId);


    }
}
