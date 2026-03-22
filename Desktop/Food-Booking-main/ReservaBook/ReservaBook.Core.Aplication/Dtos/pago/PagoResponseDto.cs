

namespace ReservaBook.Core.Aplication.Dtos.pago
{
    public class PagoResponseDto
    {

        public int Id { get; set; }
        public  DateTime Fecha { get; set; }
        public  decimal Monto { get; set; }
        public  string Estado { get; set; }
        public  string UsuarioId { get; set; }
        public int IdPedido { get; set; }
        public bool HasError    { get; set; }
        public List<string> Errors { get; set; } = new List<string>();


    }
}
