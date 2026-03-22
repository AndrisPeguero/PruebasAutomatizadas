

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Domain.Interfaces
{
    public interface IPedidoPlatoRepository : IGenericRepository<PedidoPlato>
    {

        Task<List<PedidoPlato?>>  AddRange(List<PedidoPlato>? pedidoPlatos);
        Task<List<PedidoPlato?>> GetByPedidoId(int PedidoId);

    }

}
