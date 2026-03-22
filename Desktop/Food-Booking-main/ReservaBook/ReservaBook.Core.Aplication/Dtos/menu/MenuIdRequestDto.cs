using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.menu
{
    public class MenuIdRequestDto
    {
        [Range(1,int.MaxValue, ErrorMessage = "Debes indicar un id valido,favor verificar")]
        public int Id { get; set; }

    }
}
