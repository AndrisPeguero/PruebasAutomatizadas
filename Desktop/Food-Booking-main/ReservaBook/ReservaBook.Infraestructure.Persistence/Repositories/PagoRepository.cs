
using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;
using System.Runtime.InteropServices;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class PagoRepository : GenericRepository<Pago>, IPagoRepository
    {
        private readonly ReservaBookContext _context;
        public PagoRepository(ReservaBookContext appContext) : base(appContext)
        {
            _context = appContext;
        }

        public async Task<Pago?> GetByIdPedido(int PedidoId)
        {
            

            var entity = await _context.Set<Pago>().FirstOrDefaultAsync(r => r.IdPedido ==  PedidoId);


            if (entity == null) 
            {

                return null;
            
            }

            return entity;

        }
    }
}
