using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.reserva
{
    public class CreateReservaRequestDto
    {
        public int Id { get; set; }
        public required DateOnly Fecha { get; set; }
        public required TimeOnly Hora { get; set; }
        public required string  IdUsuario { get; set; }   
        public required string Estado { get; set; }
        public required int IdMesa { get; set; }
        public int IdRestaurante { get; set; }
    
    }
}
