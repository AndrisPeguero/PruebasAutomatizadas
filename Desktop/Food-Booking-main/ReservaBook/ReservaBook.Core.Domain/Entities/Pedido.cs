

using ReservaBook.Core.Domain.Common.Enums;

namespace ReservaBook.Core.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public required DateOnly Fecha { get; set; }
        public required TimeOnly  Hora { get; set; }
        public required  EstadoPedido Estado { get; set; }
        public required decimal Total { get; set; }
         
        //foreign key y navigation property
        public ICollection<PedidoPlato> PedidoPlatos { get; set; } = new List<PedidoPlato>();

        public int IdRestaurante { get; set; } 
        public Restaurante? Restaurante { get; set; }    


        public required int  IdMesa {get;set;}
        public Mesa? Mesa { get; set; }


        public Pago? Pago { get; set; }

    }
}
