

using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.pedido
{
    public class UpdatePedidoRequestDto
    {

        [Required(ErrorMessage = "Debes indicar una fecha")]
        public required DateOnly Fecha { get; set; }
        [Required(ErrorMessage = "Debes indicar una hora")]
        public required TimeOnly Hora { get; set; }

        //id de foreign key

    }
}
