

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IPlatoMenuRepository : IGenericRepository<PlatoMenu>
    {

        Task<bool> AddRange(List<PlatoMenu> platoMenus);
        Task<PlatoMenu?> GetByIdPlatoYMenuId(int idplato, int idMenu);

    }
}
