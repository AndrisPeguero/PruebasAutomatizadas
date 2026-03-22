

using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class ReservaRepository : GenericRepository<Reserva>, IReservaResporitory
    {
        private readonly ReservaBookContext _context;


        public ReservaRepository(ReservaBookContext appContext) : base(appContext)
        {
            _context = appContext;
        }

        public async Task<List<Reserva>?> GetAllReservaByIdUsuario(string IdUsuario)
        {
            var entity = await _context.Set<Reserva>().Where(s => s.IdUsuario == IdUsuario).ToListAsync();

            if(entity == null)
            {
                return null!;
            }

            return entity;

        }
    }
}
