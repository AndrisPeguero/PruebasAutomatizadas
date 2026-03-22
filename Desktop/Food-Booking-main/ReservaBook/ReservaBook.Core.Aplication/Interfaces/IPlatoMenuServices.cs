

using ReservaBook.Core.Aplication.Dtos.menu;
using ReservaBook.Core.Aplication.Dtos.platoMenu;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IPlatoMenuServices : IGenericService<SavePlatoMenuRequesDto, SavePlatoMenuRequesDto, PlatoMenuResponseDto,PlatoMenu>
    {


        Task<List<PlatoMenuResponseDto>> AddPlatoAlMenu(int menuId, List<int> idPlatos);

        Task<PlatoMenuResponseDto> DeletePlatoDelMenu(int idMenu, int idPlato);

    }
}
