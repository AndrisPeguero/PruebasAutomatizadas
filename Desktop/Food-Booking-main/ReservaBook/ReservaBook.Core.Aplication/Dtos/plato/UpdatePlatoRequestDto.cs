

using Microsoft.AspNetCore.Http;
using ReservaBook.Core.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.plato
{
    public class UpdatePlatoRequestDto
    {

        [Required(ErrorMessage = "Debes ingresar un nombre para el plato")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar una descripcion")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "Debes de indicar un image")]
        public IFormFile? Imagen { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de indicar un precio valido para el plato")]
        public required decimal Precio { get; set; }
        [Required(ErrorMessage = "Debes indicar una categoria para el plato")]
        public PlatoCategoria Categoria { get; set; }

    }
}
