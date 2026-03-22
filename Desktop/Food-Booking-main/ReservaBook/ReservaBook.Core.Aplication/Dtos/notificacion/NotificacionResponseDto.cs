

namespace ReservaBook.Core.Aplication.Dtos.notificacion
{
    public class NotificacionResponseDto
    {

        public int Id { get; set; }
        public  string SenderId { get; set; }
        public  string ReceptorId { get; set; }
        public  string Descripcion { get; set; }
        public  DateTime Fecha { get; set; }
        public  string Tipo { get; set; }
        public  bool HasError { get; set; }
        public  List<string> Errors = new List<string>();



    }
}
