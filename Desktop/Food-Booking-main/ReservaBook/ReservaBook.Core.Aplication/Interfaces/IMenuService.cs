

using ReservaBook.Core.Aplication.Dtos.menu;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IMenuService : IGenericService<CreateMenuDto, CreateMenuDto,MenuResponseDto,Menu>
    {


        Task<DeleteMenuResponseDto?> DeleteMenuAsync(int id);


    }
}
