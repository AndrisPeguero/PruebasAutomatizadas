

namespace ReservaBook.Core.Domain.Entities
{
    public class Reserva
    {

        public int Id { get; set; }

        public required DateOnly Fecha { get; set; }
        public required TimeOnly Hora {  get; set; }
        public required string IdUsuario { get; set; }
        public required string Estado { get; set; }



        //foreing key y navigation property
         public required int IdMesa { get; set; }
         public Mesa? _Mesa { get; set; }


        public int IdRestaurante { get; set; }
        public Restaurante? Restaurante { get; set; }

        public Pago? Pago { get; set; }

    }
}
