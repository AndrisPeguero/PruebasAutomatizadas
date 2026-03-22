

namespace ReservaBook.Core.Aplication.Dtos.pdidoPlato
{
    public class PedidosPlatoResponseDto
    {
        public  int Id { get; set; }
        public  int IdPedido { get; set; }
        public  int IdPlato { get; set; }
        public  decimal PrecioUnitario { get; set; }
        public  int CantidadPlatos { get; set; }
        public decimal SubTotal { get; set; }
        public bool HasError { get; set; } = false;
        public List<string> Errors { get; set; } = new List<string>();

    }

}
