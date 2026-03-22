
namespace ReservaBook.Core.Aplication.Dtos.reserva
{
    public class ReservaResponseDto
    {       
        public int Id { get; set; }
        public  DateOnly Fecha { get; set; }
        public  TimeOnly Hora { get; set; }
        public  string Mesa { get; set; }
       public  string IdUsuario { get; set; }
        public  int CantidadPersona { get; set; }
        public  string Estado { get; set; }
        public  int IdMesa { get; set; }
        public int IdRestaurante { get; set; }
        public bool HasError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();  

    }
}
