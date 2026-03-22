
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class ReseñaEntityConfiguration : IEntityTypeConfiguration<Reseña>
    {
        public void Configure(EntityTypeBuilder<Reseña> builder)
        {
            #region basic configuration
            builder.HasKey(r => r.Id);
            builder.ToTable("Reseñas");
            #endregion



            #region property configuration
            builder.Property(r => r.Descripcion).IsRequired().HasMaxLength(1000);
            #endregion




            #region relationship configuration
            #endregion
        }
    }
}
