

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class NotificacionEntityConfiguration : IEntityTypeConfiguration<Notificacion>
    {
        public void Configure(EntityTypeBuilder<Notificacion> builder)
        {


            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("Notificaciones");
            #endregion


            #region property configuration
            builder.Property(r => r.Descripcion).IsRequired().HasMaxLength(int.MaxValue);
            builder.Property(r => r.Tipo).IsRequired().HasMaxLength(256);
            builder.Property(r => r.SenderId).IsRequired().HasMaxLength(int.MaxValue);  
            builder.Property(x => x.ReceptorId).IsRequired().HasMaxLength(int.MaxValue);
            #endregion


            #region relationShip configuration
            #endregion

        }
    }
}
