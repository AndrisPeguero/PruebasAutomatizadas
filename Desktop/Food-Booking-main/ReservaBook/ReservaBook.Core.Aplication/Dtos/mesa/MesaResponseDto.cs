

namespace ReservaBook.Core.Aplication.Dtos.mesa
{
    public class MesaResponseDto
    {
        public int Id { get; set; }
        public  string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CantidadPersonas { get; set; }
        public string Estado { get; set; } = string.Empty;
        public  int IdRestaurante { get; set; }
        public bool HasError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

    }
}
