

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ReservaBook.Core.Aplication.Dtos.mesa
{
    public class ChangeStatusMesaRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debes indicar una mesa para cambiar el estatus")]
        public required int IdMesa { get; set; }

        [Required(ErrorMessage = "Debes indicar un estado para cambiar")]
        public required string Status { get; set; } 
    }
}
