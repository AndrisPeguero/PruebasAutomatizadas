using ReservaBook.Core.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.pedido
{
    public class SavePedidoRequestDto
    {


        [Required(ErrorMessage = "Debes indicar una fecha")]
        public required DateOnly Fecha { get; set; }
        [Required(ErrorMessage = "Debes indicar un hora")]
        public required TimeOnly Hora { get; set; }

        //id de foreign key
        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar una mesa para el pedido")]
        public required int IdMesa { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debes indicar una restaurante para el pedido")]
        public required int IdRestaurante { get; set; }

    }
}
