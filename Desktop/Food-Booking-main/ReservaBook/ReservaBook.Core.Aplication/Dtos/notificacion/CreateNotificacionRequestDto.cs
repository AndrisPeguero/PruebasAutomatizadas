

namespace ReservaBook.Core.Aplication.Dtos.notificacion
{
    public class CreateNotificacionRequestDto
    {
        public int Id { get; set; }
        public required string SenderId { get; set; }
        public required string ReceptorId { get; set; }
        public required string Descripcion { get; set; }
        public required DateTime Fecha { get; set; }
        public required string Tipo { get; set; }

    }
}
