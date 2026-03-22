using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;


namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class MesaRepository : GenericRepository<Mesa>, IMesaRepository
    {
        private readonly ReservaBookContext _context;
        public MesaRepository(ReservaBookContext appContext) : base(appContext)
        {
            _context = appContext;
        }


        public async Task<bool> ChangeStatus(int idMesa,string Statu)
        {
            var entity = await _context.Set<Mesa>().FindAsync(idMesa);

            if(entity == null)
            {
                return false;
            }

            entity.Estado = Statu;
            await _context.SaveChangesAsync();
            return true;
        }





    }
}
