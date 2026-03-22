

using ReservaBook.Core.Aplication.Dtos.Reseña;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IReseñaService : IGenericService<CreateReseñaDto, CreateReseñaDto,ReseñaResponseDto,Reseña>
    {


        Task<List<ReseñaResponseDto?>> GetAllByIdRestaurnteAsync(int idRestaurante);


    }
}
