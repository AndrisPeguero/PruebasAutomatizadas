using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.platoMenu
{
    public class PlatoMenuResponseDto
    {
        public int Id { get; set; }
        public int PlatoId { get; set; }
        public int MenuId { get; set; }
        public bool HasError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

    }
}
