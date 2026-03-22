

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IPagoRepository : IGenericRepository<Pago>
    {

        Task<Pago?> GetByIdPedido(int PedidoId);

    }
}
