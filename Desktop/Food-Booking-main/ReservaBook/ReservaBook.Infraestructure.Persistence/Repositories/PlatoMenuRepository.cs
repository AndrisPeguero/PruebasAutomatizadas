using Microsoft.EntityFrameworkCore;
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
    public class PlatoMenuRepository : GenericRepository<PlatoMenu>, IPlatoMenuRepository
    {

        private readonly ReservaBookContext Context;




        public PlatoMenuRepository(ReservaBookContext appContext) : base(appContext)
        {
           Context  = appContext;
        }




        public async Task<bool> AddRange(List<PlatoMenu> platoMenus)
        {
           
            if(platoMenus == null)
            {
                return false;
            }

            await Context.Set<PlatoMenu>().AddRangeAsync(platoMenus);
            await Context.SaveChangesAsync();
            return true;

        }

        public async Task<PlatoMenu?> GetByIdPlatoYMenuId(int idplato, int idMenu)
        {

            var entity = await Context.Set<PlatoMenu>()
                         .FirstOrDefaultAsync(p => p.PlatoId == idplato && p.MenuId == idMenu);


            if (entity == null)
            {
                return null;
            }

            return entity;  

        }
    }
}
