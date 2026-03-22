

using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.pdidoPlato
{
    public class UpdatePedidoPlatoRequestDto
    {

        [Range(1, int.MaxValue, ErrorMessage = "debes indicar la cantidad de platos")]
        public required int CantidadPlatos { get; set; }

    }
}
