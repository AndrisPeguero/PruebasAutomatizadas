

using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.Reseña
{
    public class DeleteReseñaRequestDto
    {

        [Range(1,int.MaxValue,ErrorMessage = "Debes indicar la reseña a eliminar")]
        public int Id { get; set; }

    }
}
