using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.mesa
{
    public class CreateMesaRequestDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required int CantidadPersonas { get; set; }
        public required string Estado { get; set; }
        public required int IdRestaurante { get; set; }
        public string UsurioId { get; set; } = string.Empty;


    }
}
