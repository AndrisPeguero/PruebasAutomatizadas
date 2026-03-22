
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.pdidoPlato
{
    public class SavePedidoPlatoRequestDto
    {

        [Range(1, int.MaxValue,ErrorMessage = "debes indicar un pedido")]
        public required int IdPedido { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "debes indicar un plato")]
        public required int IdPlato { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "debes indicar la cantidad de platos")]
        public required int CantidadPlatos { get; set; }

     

    }
}
