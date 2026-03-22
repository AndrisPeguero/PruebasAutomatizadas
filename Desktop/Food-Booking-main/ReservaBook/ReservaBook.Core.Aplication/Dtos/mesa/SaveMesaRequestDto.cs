
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.mesa
{
    public class SaveMesaRequestDto
    {

        [Required(ErrorMessage = "Debes indicar un nombre")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "Debes indicar una descripcion")]
        public required string Descripcion { get; set; }
        [Required(ErrorMessage = "Debes indicar la cantidad de personas")]
        public required int CantidadPersonas { get; set; }


      
    }
}
