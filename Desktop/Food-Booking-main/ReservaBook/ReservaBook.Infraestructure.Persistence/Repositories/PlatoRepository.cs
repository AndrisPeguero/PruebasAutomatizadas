using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class PlatoRepository : GenericRepository<Plato>, IPlatoRepository
    {

        private readonly ReservaBookContext _context;

        public PlatoRepository(ReservaBookContext appContext) : base(appContext)
        {
            this._context = appContext;
        }


        public async Task<bool> ChangeStatus(int idPlato, string Statu)
        {
            var entity = await _context.Set<Plato>().FindAsync(idPlato);

            if (entity == null)
            {
                return false;
            }

            entity.Estado = Statu;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Plato?>> GetAllByIdMenu(int idMenu)
        {




            return [];





        }


    }
}
