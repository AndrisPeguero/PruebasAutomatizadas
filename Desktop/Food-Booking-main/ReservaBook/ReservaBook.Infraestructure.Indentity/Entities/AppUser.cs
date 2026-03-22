using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Infraestructure.Indentity.Entities
{
    public class AppUser : IdentityUser
    {

        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string ProfileImage { get; set; }
        public string RNC { get; set; } = string.Empty;


    }
}
