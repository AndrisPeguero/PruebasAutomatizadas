

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ReservaBook.Core.Aplication.Dtos.plato
{
    public class ChangeStatusPlatoRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debes indicar una mesa para cambiar el estatus")]
        public required int IdPlato { get; set; }

        [Required(ErrorMessage = "Debes indicar un estado para cambiar")]
        public required string Status { get; set; } 
    }
}
