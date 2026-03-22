

using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Entities;
using System.Reflection;

namespace ReservaBook.Infraestructure.Persistence.Contexts
{
    public class ReservaBookContext : DbContext
    {
        public ReservaBookContext(DbContextOptions<ReservaBookContext> opt) : base(opt) { }


        public DbSet<Menu> Menus { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Notificacion> Notificiones { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }      
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<PlatoMenu> PlatoMenus { get; set; }
        public DbSet<PedidoPlato> PedidoPlatos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

       



    }
}
