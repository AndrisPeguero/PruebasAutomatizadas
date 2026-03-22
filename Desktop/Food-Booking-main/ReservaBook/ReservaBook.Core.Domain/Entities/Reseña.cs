

namespace ReservaBook.Core.Domain.Entities
{
    public class Reseña
    {

        public int Id { get; set; } 
        public required string Descripcion { get; set; }
        public required int CantidadEstrella  { get; set; }



        //foreign key y navigation property
         public int IdRestaurante { get; set; }
         public Restaurante? Restaurante { get; set; }


    }
}
