

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.restaurante
{
    public class UpdateRestauranteRequestDto
    {
      
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar una direccion")]
        public required string Direccion { get; set; }
     
        public string? Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes indicar un horario de inicio")]
        public required  TimeOnly HorarioInicio { get; set; }

        [Required(ErrorMessage = "Debes indicar un horario de fin")]
        public required TimeOnly HorarioFin { get; set; }

        [Required(ErrorMessage = "Debes indicar una especialidad gastronomica valida")]
        public required string EspecialidadGastronomica { get; set; }

        public  IFormFile? Imagen { get; set; }


    }
}
