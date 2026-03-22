using ReservaBook.Core.Aplication.Dtos.email;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailRequestDto dto);
    }
}