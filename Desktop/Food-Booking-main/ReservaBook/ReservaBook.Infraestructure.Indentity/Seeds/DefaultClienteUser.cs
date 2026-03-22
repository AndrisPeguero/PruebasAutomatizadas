using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservaBook.Core.Domain.Common.Enums;
using ReservaBook.Infraestructure.Indentity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Infraestructure.Indentity.Seeds
{
    public static class DefaultClienteUser
    {

        public static async Task SeedAsync(UserManager<AppUser> userManager)
        {

            AppUser user = new()
            {

                Name = "Stivenson",
                LastName = "Ramirez DEV",
                UserName = "UserClient",
                EmailConfirmed = true,
                PhoneNumber = "8291210020",
                Email = "ClientNoReply@gmail.com",
                PhoneNumberConfirmed = true,
                ProfileImage = "",

            };
          


            if(await userManager.Users.AllAsync(u => u.Id != user.Id))
            {
                 var entityUser = await userManager.FindByEmailAsync(user.Email);

                if (entityUser == null)
                {
                    await userManager.CreateAsync(user,"901KIO@v");
                    await userManager.AddToRoleAsync(user,UserRoles.Cliente.ToString());

                }
               
           
            }


        }


    }
}
