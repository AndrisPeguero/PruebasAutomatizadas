using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservaBook.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Infraestructure.Persistence.EntityConfigurations
{
    public class RestauranteEntityConfiguration : IEntityTypeConfiguration<Restaurante>
    {
        public void Configure(EntityTypeBuilder<Restaurante> builder)
        {

            #region basic configuration
            builder.HasKey(e => e.Id);
            builder.ToTable("Restaurantes");
            #endregion



            #region property configuration
            builder.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Telefono).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Direccion).IsRequired().HasMaxLength(150);
            builder.Property(r => r.EspecialidadGastronomica).IsRequired().HasMaxLength(250);
            builder.Property(r => r.UsuarioId).IsRequired().HasMaxLength(1000);
            builder.Property(r => r.Imagen).IsRequired().HasMaxLength(int.MaxValue);
            #endregion



            #region relationship
            builder.HasMany<Reserva>(r => r.Reservas)
                   .WithOne(r => r.Restaurante)
                   .HasForeignKey(r => r.IdRestaurante)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Mesa>(r => r.Mesas)
                   .WithOne(r => r.Restaurante)
                   .HasForeignKey(r => r.IdRestaurante)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany<Pedido>(r => r.Pedidos)
                   .WithOne(r => r.Restaurante)
                   .HasForeignKey(r => r.IdRestaurante)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany<Reseña>(r => r.Reseñas)
                   .WithOne(r => r.Restaurante)
                   .HasForeignKey(r => r.IdRestaurante)
                    .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
