using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.mesa
{
    public class MesaRequestIdDto
    {
        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar un id para realizar la operacion")]
        public int Id { get; set; }

    }
}
