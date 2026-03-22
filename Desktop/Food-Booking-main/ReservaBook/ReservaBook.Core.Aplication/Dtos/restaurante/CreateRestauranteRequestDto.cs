

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.restaurante
{
    public class CreateRestauranteRequestDto
    {
        public int Id { get; set; }
        public required string UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public required string Telefono { get; set; }
        public required TimeOnly HorarioInicio { get; set; }
        public required TimeOnly HorarioFin { get; set; }
        public required string EspecialidadGastronomica { get; set; }
        public required string Imagen { get; set; }

    }
}
