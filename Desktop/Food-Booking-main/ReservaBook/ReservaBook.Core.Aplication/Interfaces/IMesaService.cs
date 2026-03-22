
using ReservaBook.Core.Aplication.Dtos.mesa;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IMesaService : IGenericService<CreateMesaRequestDto, CreateMesaRequestDto,MesaResponseDto,Mesa>
    {

        Task<bool> ChangeStatus(int idMesa, string Status);

    }
}
