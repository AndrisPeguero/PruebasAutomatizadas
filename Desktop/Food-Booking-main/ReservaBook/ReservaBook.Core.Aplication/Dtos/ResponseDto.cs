using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos
{
    public class ResponseDto
    {
        public bool HasError { get; set; }
        public List<string>? Errors { get; set; }

    }
}
