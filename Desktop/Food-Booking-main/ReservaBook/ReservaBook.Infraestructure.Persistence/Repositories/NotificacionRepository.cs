

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;

namespace ReservaBook.Infraestructure.Persistence.Repositories
{
    public class NotificacionRepository : GenericRepository<Notificacion>, INotificacionRepository
    {

        private readonly ReservaBookContext _context;



        public NotificacionRepository(ReservaBookContext appContext) : base(appContext)
        {
            _context = appContext;  
        }

        public async Task<List<Notificacion?>> GetByReceptorId(string receptor)
        {
            var entities = await _context.Set<Notificacion>().Where(n => n.ReceptorId == receptor || n.SenderId == receptor).ToListAsync();


            if(entities == null || entities.Count <= 0)
            {
                return [];
            }


            return entities!;
           
        }
    }
}
