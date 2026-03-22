

namespace ReservaBook.Core.Aplication.Dtos.Reseña
{
    public class ReseñaResponseDto
    {
        public int Id { get; set; }
        public  string Descripcion { get; set; }
        public  int CantidadEstrella { get; set; }

        public int IdRestaurante { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public  bool HasErrors { get; set; }


    }
}
