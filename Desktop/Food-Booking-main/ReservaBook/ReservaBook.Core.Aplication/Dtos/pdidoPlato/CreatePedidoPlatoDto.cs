

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Dtos.pdidoPlato
{
    public class CreatePedidoPlatoDto
    {
        public required int Id { get; set; }
        public required int IdPedido { get; set; }       
        public required int IdPlato { get; set; }
        public required decimal PrecioUnitario { get; set; }
        public required int CantidadPlatos { get; set; }


    }
}
