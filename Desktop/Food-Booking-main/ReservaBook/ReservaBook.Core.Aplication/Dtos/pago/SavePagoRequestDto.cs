

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.pago
{
    public class SavePagoRequestDto
    {

        [Range(1,int.MaxValue,ErrorMessage = "Debes introduccir el monto del pago")]
        public required decimal Monto { get; set; }
        [Required(ErrorMessage = "Debes indicar el pedido que  quires pagar")]
        public int IdPedido { get; set; }

    }
}
