

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class PedidoEntityConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {

            #region basic configuration
            builder.HasKey(r => r.Id);
            builder.ToTable("Pedidos");
            #endregion



            #region property configuration
            builder.Property(r => r.Total).HasPrecision(22, 2);
            #endregion



            #region relationShip configuration
            builder.HasOne(r => r.Pago)
                    .WithOne(r => r.Pedido)
                    .HasForeignKey<Pago>(r => r.IdPedido)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion


        }
    }
}
