using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.platoMenu
{
    public class DeletePlatoMenuRequestDto
    {

        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar un plato valido")]
        public int IdPlato { get; set; }



        [Range(1, int.MaxValue, ErrorMessage = "Debes indicar un menu valido")]
        public int IdMenu { get; set; }


    }
}
