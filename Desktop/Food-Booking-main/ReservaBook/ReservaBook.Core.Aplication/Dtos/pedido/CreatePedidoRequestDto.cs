

using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Dtos.pedido
{
    public class CreatePedidoRequestDto
    {
        public int Id { get; set; }
        public required DateOnly Fecha { get; set; }
        public required TimeOnly Hora { get; set; }
        public required EstadoPedido Estado { get; set; }
        public required decimal Total { get; set; }
        public string ClienteId { get; set; } = string.Empty;

        //id de foreign key
        public required int IdRestaurante { get; set; }
        public required int IdMesa { get; set; }
      
    }
}
