

using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Dtos.plato
{
    public class CreatePlatoRequestDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public string? Imagen { get; set; }
        public required decimal Precio { get; set; }
        public required string Categoria { get; set; }
        public required string Estado { get; set; }



        //foreign key y navigation property
        public int? IdMenu { get; set; }
     
    }
}
