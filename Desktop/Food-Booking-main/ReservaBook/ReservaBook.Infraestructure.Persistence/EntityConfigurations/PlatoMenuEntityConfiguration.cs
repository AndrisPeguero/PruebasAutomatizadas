using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class PlatoMenuEntityConfiguration : IEntityTypeConfiguration<PlatoMenu>
    {
        public void Configure(EntityTypeBuilder<PlatoMenu> builder)
        {
            #region basic configuration
            builder.HasKey(x => x.Id);
            builder.ToTable("PlatoMenus");
            #endregion



            #region property Configuration
            builder.Property(r => r.PlatoId).IsRequired();
            builder.Property(r => r.MenuId).IsRequired();
            #endregion



            #region relationShip configuration
            builder.HasOne(r => r.Plato)
                   .WithMany(r => r.PlatoMenus)
                   .HasForeignKey(r => r.PlatoId);

            builder.HasOne(r => r.Menu)
                   .WithMany(r => r.PlatoMenus)
                   .HasForeignKey(r => r.MenuId);

            #endregion


        }
    }
}
