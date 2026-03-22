

using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.reserva
{
    public class SaveReservaRequestDto
    {

        [Required(ErrorMessage = "Debes indicar la fecha de la reserva")]
        public required DateOnly Fecha { get; set; }
        [Required(ErrorMessage = "Debes indicar la hora de la reserva")]
        public required TimeOnly Hora { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar una mesa")]
        public required int IdMesa { get; set; }
        [Range(1,int.MaxValue, ErrorMessage = "Debes indicar un restaurante")]
        public int IdRestaurante { get; set; }

    }
}
