using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;


namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class ReservaEntityConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {

            #region basic configuration
            builder.HasKey(e => e.Id);
            builder.ToTable("Reservas");
            #endregion



            #region property configuration
            builder.Property(r => r.Estado).IsRequired().HasMaxLength(200);
            #endregion


            #region relationships
            #endregion



        }
    }
}
