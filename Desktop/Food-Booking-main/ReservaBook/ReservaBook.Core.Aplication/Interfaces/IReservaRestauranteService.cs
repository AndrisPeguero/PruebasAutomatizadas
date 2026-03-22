

using ReservaBook.Core.Aplication.Dtos.reserva;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IReservaRestauranteService : IGenericService<CreateReservaRequestDto, CreateReservaRequestDto,ReservaResponseDto,Reserva>
    {
        Task<List<ReservaResponseDto>?> GetAllReservaByIdUsuario(string IdUsuario);

    }
}
