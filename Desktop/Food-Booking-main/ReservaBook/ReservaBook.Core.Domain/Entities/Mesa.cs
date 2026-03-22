

namespace ReservaBook.Core.Domain.Entities
{
    public class Mesa
    {
        public int Id { get; set; }
        public  required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required int CantidadPersonas { get; set; }
        public required string Estado { get; set; }
        

        // foreign key y navigation property
        public required int IdRestaurante { get; set; }
        public  Restaurante? Restaurante { get; set; }


        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<Reserva>? Reservas { get; set; } = new List<Reserva>();

    }
}
