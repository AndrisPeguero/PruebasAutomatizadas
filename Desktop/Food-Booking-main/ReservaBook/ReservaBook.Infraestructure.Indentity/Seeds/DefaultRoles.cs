

using Microsoft.AspNetCore.Identity;
using ReservaBook.Core.Domain.Common.Enums;

namespace ReservaBook.Infraestructure.Indentity.Seeds
{
    public static class DefaultRoles
    {

        public static async Task seedAsync(RoleManager<IdentityRole> roleManager)
        {

            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Propietario.ToString()));
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Cliente.ToString()));


       
        }

    }
}
