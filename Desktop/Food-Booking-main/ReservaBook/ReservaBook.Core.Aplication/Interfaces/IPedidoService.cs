

using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IPedidoService : IGenericService<CreatePedidoRequestDto, CreatePedidoRequestDto,PedidoResponseDto,Pedido>
    {

        Task<bool> ChangeStatus(int IdPedido, EstadoPedido pedido);

    }

}
