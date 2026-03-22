

using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class RestauranteRepository : GenericRepository<Restaurante>, IRestauranteRepository
    {
        private readonly ReservaBookContext _Context;

        public RestauranteRepository(ReservaBookContext appContext) : base(appContext)
        {
            _Context = appContext;
        }

        public async Task<Restaurante?> GetByUserId(string UserId)
        {

            var entity = await _Context.Set<Restaurante>().FirstOrDefaultAsync(r => r.UsuarioId == UserId);

            if (entity == null)
            {
                return null;
               
            }


            return entity;

        }
    }
}
