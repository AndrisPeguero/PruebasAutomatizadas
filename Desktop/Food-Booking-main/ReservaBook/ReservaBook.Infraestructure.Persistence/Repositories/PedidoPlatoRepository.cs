

using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class PedidoPlatoRepository : GenericRepository<PedidoPlato>, IPedidoPlatoRepository
    {

        private readonly ReservaBookContext _context;

        public PedidoPlatoRepository(ReservaBookContext appContext) : base(appContext)
        {
            _context = appContext;
        }

        public async Task<List<PedidoPlato?>> AddRange(List<PedidoPlato>? pedidoPlatos)
        {

            if (pedidoPlatos == null && !pedidoPlatos!.Any())
            {
                return [];
            }


            await _context.Set<PedidoPlato>().AddRangeAsync(pedidoPlatos);
            await _context.SaveChangesAsync();
            return pedidoPlatos!;
        }

        public async Task<List<PedidoPlato?>> GetByPedidoId(int PedidoId)
        {
           
            

                var entities = await _context.Set<PedidoPlato>()
                                           .Where(p => p.IdPedido == PedidoId).ToListAsync();

                if(entities.Count() > 0)
                {
                    return entities;        
                }


                return [];
            
        }
    }
}
