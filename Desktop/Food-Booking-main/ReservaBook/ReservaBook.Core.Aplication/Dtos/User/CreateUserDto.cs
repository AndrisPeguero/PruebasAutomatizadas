

using Microsoft.AspNetCore.Http;
using ReservaBook.Core.Domain.Common.Enums;

namespace ReservaBook.Core.Aplication.Dtos.User
{
    public class CreateUserDto
    {
    
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public required IFormFile ProfileImage { get; set; }
        public required UserRoles Role { get; set; }
        public string? RNC { get; set; }


    }
}
