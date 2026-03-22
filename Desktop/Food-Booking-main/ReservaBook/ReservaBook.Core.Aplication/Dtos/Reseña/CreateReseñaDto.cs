

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Dtos.Reseña
{
    public class CreateReseñaDto
    {


        public int Id { get; set; }
        public required string Descripcion { get; set; }
        public required int CantidadEstrella { get; set; }
        public string ClienteId { get; set; } = string.Empty;

        //foreign key y navigation property
        public int IdRestaurante { get; set; }
 

    }
}
