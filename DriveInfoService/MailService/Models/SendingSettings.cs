namespace MailService.Models
{
    public class SendingSettings
    {
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int SenderPort { get; set; }
        public string SenderHost { get; set; }
        public string Message { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
    }
}
