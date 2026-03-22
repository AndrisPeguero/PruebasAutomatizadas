

namespace ReservaBook.Core.Domain.Entities
{
    public class PedidoPlato
    {
        public required int Id { get; set; }
        public required int IdPedido { get; set; }
        public Pedido? Pedido { get; set; }
        public required int IdPlato { get; set; }
        public Plato? Plato { get; set; }
        public required decimal PrecioUnitario { get; set; }
        public required  int CantidadPlatos {get;set;}

    }
}
