using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.restaurante
{
    public class RestauranteResponseDto
    {
        public int Id { get; set; }
        public  string UsuarioId { get; set; } = string.Empty;
        public  string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public  TimeOnly HorarioInicio { get; set; }
        public  TimeOnly HorarioFin { get; set; }
        public string EspecialidadGastronomica { get; set; } = string.Empty;
        public  string? Imagen { get; set; }
        public bool HasError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool IsCreated { get; set; }
         
    }
}
