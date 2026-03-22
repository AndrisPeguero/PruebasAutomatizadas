using ReservaBook.Core.Domain.Common.Enums;


namespace ReservaBook.Core.Aplication.Dtos.pedido
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public  DateOnly Fecha { get; set; }
        public  TimeOnly Hora { get; set; }
        public  EstadoPedido Estado { get; set; }

        public  decimal Total { get; set; }

        //id de foreign key
        public  int IdRestaurante { get; set; }
        public  int IdMesa { get; set; }

        public bool HasError { get; set; }
        public  List<string> Errors { get; set; } = new List<string>(); 

    }
}
