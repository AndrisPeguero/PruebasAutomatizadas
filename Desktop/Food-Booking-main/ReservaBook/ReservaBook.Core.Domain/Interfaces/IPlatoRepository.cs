using ReservaBook.Core.Domain.Entities;


namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IPlatoRepository : IGenericRepository<Plato>
    {

        Task<bool> ChangeStatus(int idPlato, string Statu);
        Task<List<Plato?>> GetAllByIdMenu(int idMenu);

    }
}
