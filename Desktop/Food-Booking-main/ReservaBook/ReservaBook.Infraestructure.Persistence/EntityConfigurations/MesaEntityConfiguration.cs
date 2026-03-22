
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class MesaEntityConfiguration : IEntityTypeConfiguration<Mesa>
    {
        public void Configure(EntityTypeBuilder<Mesa> builder)
        {

            #region basic configuration
            builder.HasKey(m => m.Id);
            builder.ToTable("Mesas");
            #endregion




            #region property configuration
            builder.Property(r => r.Nombre).IsRequired().HasMaxLength(256);
            builder.Property(r => r.Descripcion).IsRequired().HasMaxLength(256);
            builder.Property(r => r.CantidadPersonas).IsRequired();
            builder.Property(r => r.Estado).IsRequired().HasMaxLength(100);
            #endregion



            #region relationships configuration
            builder.HasMany<Reserva>(r => r.Reservas)
                   .WithOne(r => r._Mesa)
                   .HasForeignKey(r => r.IdMesa)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Pedido>(r => r.Pedidos)
                          .WithOne(r => r.Mesa)
                          .HasForeignKey(r => r.IdMesa);
            #endregion

        }
    }
}
