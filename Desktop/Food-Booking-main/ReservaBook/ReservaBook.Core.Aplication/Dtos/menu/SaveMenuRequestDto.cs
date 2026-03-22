using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.menu
{
    public class SaveMenuRequestDto
    {

        [Required(ErrorMessage = "Debes indicar un nombre para el menu")]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Debes indicar una descripcion para el menu")]
        public required string Descripcion { get; set; }

    
    }
}
