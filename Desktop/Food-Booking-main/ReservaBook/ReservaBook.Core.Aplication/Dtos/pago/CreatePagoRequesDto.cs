

using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Dtos.pago
{
    public class CreatePagoRequesDto
    {


        public int Id { get; set; }
        public required DateTime Fecha { get; set; }
        public required decimal Monto { get; set; }
        public required string Estado { get; set; }
        public required string UsuarioId { get; set; }
        public int IdPedido { get; set; }


   
    }



}


