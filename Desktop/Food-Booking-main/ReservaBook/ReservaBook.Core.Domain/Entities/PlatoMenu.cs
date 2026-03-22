
namespace ReservaBook.Core.Domain.Entities
{
    public class PlatoMenu
    {
        public int Id { get; set; }
        public int PlatoId { get; set; }
        public Plato? Plato { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }

    }
}
