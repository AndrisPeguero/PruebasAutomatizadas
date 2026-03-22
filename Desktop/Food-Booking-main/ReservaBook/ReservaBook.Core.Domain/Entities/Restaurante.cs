

using System.Reflection.Metadata.Ecma335;

namespace ReservaBook.Core.Domain.Entities
{
    public class Restaurante
    {

        public int Id { get; set; } 
        public required string UsuarioId { get; set; } 
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public required TimeOnly HorarioInicio { get; set; }
        public required TimeOnly HorarioFin { get; set; }
        public required string EspecialidadGastronomica { get; set; }
        public required string Imagen {  get; set; }


        //Navigation property
        public ICollection<Mesa> Mesas {  get; set; } = new List<Mesa>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<Reseña> Reseñas { get; set; } = new List<Reseña>();


    }
}
