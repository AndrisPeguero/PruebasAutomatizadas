using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Core.Domain.Settings
{
    public class MailSettings
    {
        public required string EmailFrom { get; set; } 
        public  required string SmtpHost  { get; set; }
        public  required int SmtpPort { get; set; }
        public  required string SmtpUser { get; set; }
        public required string SmtpPass { get; set; }
        public required string DisplayName { get; set; }


    }
}
