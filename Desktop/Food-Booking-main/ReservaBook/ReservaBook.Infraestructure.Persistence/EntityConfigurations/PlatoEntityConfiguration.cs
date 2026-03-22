

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class PlatoEntityConfiguration : IEntityTypeConfiguration<Plato>
    {
        public void Configure(EntityTypeBuilder<Plato> builder)
        {

            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Platos");
            #endregion


            #region property configuration
            builder.Property(r => r.Nombre).IsRequired().HasMaxLength(256); 
            builder.Property(r => r.Descripcion).IsRequired().HasMaxLength(400);
            builder.Property(r => r.Imagen).HasMaxLength(int.MaxValue);
            builder.Property(r => r.Categoria).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Precio).IsRequired().HasPrecision(22,2);
            #endregion


            #region relationship configuration
            #endregion



        }
    }
}
