using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.Reseña
{
    public class GetReseñasByIdRestauranteRequestDto
    {

        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar el restaurante para ver su reseñas")]
        public required int Id { get; set; }    

    }
}
