

using Microsoft.AspNetCore.Http;

namespace ReservaBook.Core.Aplication.Dtos.User
{
    public class EditUserRequestMinDto
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public  string? Email { get; set; }
        public  string? UserName { get; set; }
        public  string? Password { get; set; }
        public string? Phone { get; set; }
        public IFormFile? ProfileImage { get; set; }
     
    }
}
