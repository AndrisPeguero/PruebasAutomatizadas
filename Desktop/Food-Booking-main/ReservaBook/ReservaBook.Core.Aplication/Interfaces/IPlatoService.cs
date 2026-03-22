

using ReservaBook.Core.Aplication.Dtos.plato;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IPlatoService : IGenericService<CreatePlatoRequestDto, CreatePlatoRequestDto,PlatoResponseDto,Plato>
    {

        Task<bool> ChangeStatus(int idPlato, string Status);

    }
}
