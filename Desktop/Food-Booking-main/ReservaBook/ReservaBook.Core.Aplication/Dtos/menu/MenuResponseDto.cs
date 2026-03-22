

using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Core.Aplication.Dtos.menu
{
    public class MenuResponseDto
    {
        public int Id { get; set; }
        public  string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public  List<string>? Errors { get; set; } = new List<string>();
        public  bool HasErrors { get; set; }
        public bool IsCreated { get; set; }
        public  int IdRestaurante { get; set; }


    }
}
