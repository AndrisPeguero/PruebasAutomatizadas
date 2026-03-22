

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Settings;
using ReservaBook.Infraestructure.Shared.Services;
using System.ComponentModel.DataAnnotations;

namespace ReservaBook.Infraestructure.Shared
{
    public static class ServicesRegistration
    {

       

        public static void AddEmailServicesIOC(this IServiceCollection service, IConfiguration connfig)
        {


            #region email configuration
            service.Configure<MailSettings>(connfig.GetSection("MailSettings"));
            #endregion




            #region services configurationIOC
            service.AddScoped<IEmailService,EmailService>();
            #endregion


         
        }


    }
}
