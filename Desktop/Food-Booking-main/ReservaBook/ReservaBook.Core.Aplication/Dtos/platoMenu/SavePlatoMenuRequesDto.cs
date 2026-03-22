using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.platoMenu
{
    public class SavePlatoMenuRequesDto
    {
     
        [Required(ErrorMessage = "Debes indicar uno o varios plato")]
        public List<int> IdPlatos { get; set; } = new List<int>();
        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar un menu valido")]
        public int IdMenu { get; set; }
    }
}
