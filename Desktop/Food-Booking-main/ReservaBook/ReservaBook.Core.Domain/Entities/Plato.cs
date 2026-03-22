using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Domain.Entities
{
    public class Plato
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public string? Imagen {  get; set; }
        public required decimal Precio { get; set; }
        public required string Categoria { get; set; }
        public required string Estado { get; set; } 


        //foreign key y navigation property
        
        public ICollection<PlatoMenu> PlatoMenus { get; set; } = new List<PlatoMenu>();
        public ICollection<PedidoPlato> PedidoPlatos { get; set; } = new List<PedidoPlato>();
         



    }
}
