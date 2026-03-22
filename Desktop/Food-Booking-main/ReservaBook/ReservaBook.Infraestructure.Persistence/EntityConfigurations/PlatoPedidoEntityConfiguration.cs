

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class PlatoPedidoEntityConfiguration : IEntityTypeConfiguration<PedidoPlato>
    {


        public void Configure(EntityTypeBuilder<PedidoPlato> builder)
        {

            #region basic configuration
            builder.HasKey(r => r.Id);
            builder.ToTable("PlatoPedidos");
            #endregion



            #region property configuration
            #endregion




            #region relationship configuration
            builder.HasOne(r => r.Pedido)
                   .WithMany(r => r.PedidoPlatos)
                   .HasForeignKey(r => r.IdPedido);


            builder.HasOne(r => r.Plato)
                 .WithMany(r => r.PedidoPlatos)
                 .HasForeignKey(r => r.IdPlato);
            #endregion



        }
    }
}
