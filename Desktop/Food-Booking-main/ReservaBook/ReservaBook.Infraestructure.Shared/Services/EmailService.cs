using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ReservaBook.Core.Aplication.Dtos.email;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Settings;




namespace ReservaBook.Infraestructure.Shared.Services
{
    public class EmailService : IEmailService
    {

        public readonly MailSettings _mailSettings;
        public readonly ILogger<EmailService> _logger;


        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {

            _mailSettings = mailSettings.Value;
            _logger = logger;

        }




        public async Task<bool> SendAsync(EmailRequestDto? dto)
        {
            try
            {
                if (dto == null)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(dto.To)
                   || string.IsNullOrEmpty(dto.Subject)
                   || string.IsNullOrEmpty(dto.HtmlBody)
                  )
                {

                    return false;
                }



                dto.ToRange.Add(dto.To ?? "");


                MimeMessage email = new()
                {

                    Sender = MailboxAddress.Parse(_mailSettings.EmailFrom),
                    Subject = dto.Subject


                };



                foreach (var toItem in dto.ToRange ?? [])
                {
                    email.To.Add(MailboxAddress.Parse(toItem));
                }



                BodyBuilder builder = new()
                {
                    HtmlBody = dto.HtmlBody
                };


                email.Body = builder.ToMessageBody();


                using MailKit.Net.Smtp.SmtpClient smptClient = new();
                await smptClient.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                await smptClient.AuthenticateAsync(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smptClient.SendAsync(email);
                await smptClient.DisconnectAsync(true);
                return true;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An ocurred an error with send Email", ex);
                return false;

            }

        }





    }
}
