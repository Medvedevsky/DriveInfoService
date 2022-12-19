using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Models
{
    public class SendingSettings
    {
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public int SenderPort { get; set; }
        public string SenderHost { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
    }
}
