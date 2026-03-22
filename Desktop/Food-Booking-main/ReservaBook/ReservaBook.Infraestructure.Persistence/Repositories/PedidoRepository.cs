using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {

        private readonly ReservaBookContext _context;

        public PedidoRepository(ReservaBookContext appContext) : base(appContext)
        {
            this._context = appContext;
        }


        public async Task<bool> ChangeStatus(int idMesa, EstadoPedido estado)
        {
            var entity = await _context.Set<Pedido>().FindAsync(idMesa);

            if (entity == null)
            {
                return false;
            }

            entity.Estado = estado;
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
