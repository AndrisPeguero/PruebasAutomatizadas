using ReservaBook.Core.Aplication.Dtos.pago;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IPagoService : IGenericService<CreatePagoRequesDto, CreatePagoRequesDto,PagoResponseDto,Pago>
    {
        Task<PagoResponseDto?> GetBypedidoId(int pedidoId);

    }
}
