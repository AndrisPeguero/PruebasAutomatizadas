using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.menu
{
    public class CreateMenuDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required int IdRestaurante { get; set; }
        public string IdUsuario { get; set; } = string.Empty;

    }
}
