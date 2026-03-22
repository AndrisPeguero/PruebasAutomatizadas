

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class PagoEntityConfiguration : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {


            #region basic configuration 
            builder.HasKey(x => x.Id);
            builder.ToTable("Pagos");
            #endregion


            #region property Configuration
            builder.Property(p => p.Estado).IsRequired().HasMaxLength(60);
            builder.Property(p => p.Monto).IsRequired().HasPrecision(22,2);
            #endregion




            #region relationShips configuration
     
            #endregion








        }
    }
}
