using ReservaBook.Core.Domain.Entities;


namespace ReservaBook.Core.Domain.Interfaces
{
    public interface INotificacionRepository : IGenericRepository<Notificacion>
    {

        Task<List<Notificacion?>> GetByReceptorId(string receptor);

    }
}
