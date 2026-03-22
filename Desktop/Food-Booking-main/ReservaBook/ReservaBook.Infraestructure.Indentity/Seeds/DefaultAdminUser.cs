

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Infraestructure.Indentity.Entities;

namespace ReservaBook.Infraestructure.Indentity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {


            AppUser user = new()
            {
                Name = "Kelvin",
                LastName = "Diaz Ramirez",
                UserName = "KIO-Admin",
                EmailConfirmed = true,
                PhoneNumber = "8290010020",
                Email = "Kervindiaramirez@gmail.com",
                PhoneNumberConfirmed = true,
                ProfileImage = "",
                
            };


            if (await userManager.Users.AllAsync(u => u.Id != user.Id))
            {

                var EntityUser = await userManager.FindByEmailAsync(user.Email);
                if (EntityUser == null)
                {

                  var result = await  userManager.CreateAsync(user,"123@Admn");
                  await userManager.AddToRoleAsync(user,UserRoles.Admin.ToString());

                }

            }

       
        }
    }
}
