using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.restaurante
{
    public class SaveRestauranteRequestDto
    {

        [Required(ErrorMessage = "Debes ingresar nombre")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar una direccion")]
        public required string Direccion { get; set; }
        [Required(ErrorMessage = "Debes ingresar un numero de telefono")]
        public required string Telefono { get; set; }
        [Required(ErrorMessage = "Debes indicar un horario de inicio valido")]
        public required TimeOnly HorarioInicio { get; set; }
        [Required(ErrorMessage = "Debes indicar un horario de fin valido")]
        public required TimeOnly HorarioFin { get; set; }
        [Required(ErrorMessage = "Debes indicar una especialidad gastronomica valida")]
        public required string EspecialidadGastronomica { get; set; }
        [Required(ErrorMessage = "Debes indicar una imagen")]
        public required IFormFile Imagen { get; set; }

    }
}
