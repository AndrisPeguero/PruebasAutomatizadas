
namespace ReservaBook.Core.Aplication.Dtos.plato
{
    public class PlatoResponseDto
    {

        public int Id { get; set; }
        public  string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Imagen { get; set; }
        public  decimal Precio { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public bool HasError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();



        //foreign key y navigation property
        public int? IdMenu { get; set; }

    }



}

