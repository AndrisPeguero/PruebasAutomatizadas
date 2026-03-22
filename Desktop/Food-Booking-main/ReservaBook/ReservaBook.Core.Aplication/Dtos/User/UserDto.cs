

namespace ReservaBook.Core.Aplication.Dtos.User
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }   
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public string Phone {  get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public string? ProfileImage { get; set; }
        public required string Role {  get; set; }
     
    }
}
