using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Aplication.Dtos.Reseña
{
    public class SaveReseñaRequestDto
    {


        [Required(ErrorMessage = "Debes indicar una descripcion para la reseña")]
        public required string Descripcion { get; set; }

        [Range(1,5,ErrorMessage = "Debes indicar una cantidad de estrella valida,(1-5)")]
        public required int CantidadEstrella { get; set; }

        [Range(1, int.MaxValue,ErrorMessage = "Debes indicar el restaurante")]
        public int IdRestaurante { get; set; }

     
        


    }
}
