

using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class ReseñaRepository : GenericRepository<Reseña>, IReseñaRepository
    {
        private readonly ReservaBookContext _context;
      
        public ReseñaRepository(ReservaBookContext appContext) : base(appContext)
        {


            this._context = appContext;


        }


        public async  Task<List<Reseña>> GetAllReseñaByIdRestaurante(int idRestaurante)
        {

            var entities = await _context.Set<Reseña>()
                          .Where(s => s.IdRestaurante == idRestaurante).ToListAsync();


            if(entities == null || entities.Count <= 0)
            {
                return [];
            }



            return entities;

                    
        }
    }
}
