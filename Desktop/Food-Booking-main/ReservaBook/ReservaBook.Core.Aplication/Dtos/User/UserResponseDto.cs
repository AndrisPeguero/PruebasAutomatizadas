

namespace ReservaBook.Core.Aplication.Dtos.User
{
    public class UserResponseDto
    {

        public bool HasError { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();
        public string? Message { get; set; }


    }
}
