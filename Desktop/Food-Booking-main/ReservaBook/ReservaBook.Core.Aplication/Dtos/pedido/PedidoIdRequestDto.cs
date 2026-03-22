

using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.pedido
{
    public class PedidoIdRequestDto
    {

        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar un pedido valido")]
        public required int Id { get; set; }

    }
}
