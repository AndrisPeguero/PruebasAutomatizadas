

namespace ReservaBook.Core.Aplication.Dtos.User
{
    public class EditResponseDto
    {

        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public List<string>? Roles { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsVerified { get; set; }
        public bool HasError { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();


    }

}
