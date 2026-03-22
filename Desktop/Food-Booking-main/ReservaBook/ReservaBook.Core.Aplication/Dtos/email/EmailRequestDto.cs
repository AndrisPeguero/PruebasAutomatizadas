

namespace ReservaBook.Core.Aplication.Dtos.email
{
    public class EmailRequestDto
    {

        public string To { get; set; } = string.Empty;
        public required string Subject { get; set; }
        public required string HtmlBody { get; set; }
        public List<string> ToRange { get; set; } = new List<string>();


    }
}
