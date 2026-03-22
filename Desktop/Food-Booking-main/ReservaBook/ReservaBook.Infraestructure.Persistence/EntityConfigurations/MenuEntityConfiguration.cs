
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class MenuEntityConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {

            #region basic configuration
            builder.HasKey(e => e.Id);
            builder.ToTable("Menus");
            #endregion




            #region property configuration
            builder.Property(m => m.Nombre).IsRequired().HasMaxLength(256);
            #endregion




            #region relationShips
            
            #endregion

        }
    }
}
