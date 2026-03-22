using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservaBook.Infraestructure.Indentity.Entities;


namespace ReservaBook.Infraestructure.Indentity.Contexts
{
    public class IdentityContext : IdentityDbContext<AppUser>
    {

        public IdentityContext(DbContextOptions<IdentityContext> opt) : base(opt) { }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            //fluent api
            base.OnModelCreating(builder);

            //esquema para identity
            builder.HasDefaultSchema("Identity");



            //nombre de tablas
            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UsersLogin");

        }


    }
}
