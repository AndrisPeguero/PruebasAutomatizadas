

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IMesaRepository : IGenericRepository<Mesa>
    {

        Task<bool> ChangeStatus(int idMesa,string Statu);

    }
}
