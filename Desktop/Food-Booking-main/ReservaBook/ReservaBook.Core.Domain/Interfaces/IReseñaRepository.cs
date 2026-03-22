

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IReseñaRepository : IGenericRepository<Reseña>
    {


        Task<List<Reseña>> GetAllReseñaByIdRestaurante(int idRestaurante);


    }

}
