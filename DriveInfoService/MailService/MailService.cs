using MailKit.Net.Smtp;
using MailService.Models;
using MimeKit;

namespace MailService
{
    public static class MailService
    {
        public static async Task<bool> SendEmailAsync(SendingSettings settings, string recipientEmail)
        {
            using SmtpClient client = new();

            await client.ConnectAsync(settings.SenderHost, settings.SenderPort, false);
            await client.AuthenticateAsync(settings.SenderEmail, settings.SenderEmailPassword);

            try
            {
                MimeMessage emailMessage = new();
                emailMessage.From.Add(new MailboxAddress(settings.Sender, settings.SenderEmail));
                emailMessage.To.Add(MailboxAddress.Parse(recipientEmail));
                emailMessage.Subject = settings.Subject;
            
                BodyBuilder builder = new();
            
                if (string.IsNullOrWhiteSpace(settings.Message) == false)
                {
                    builder.TextBody = settings.Message;
                }
            
                emailMessage.Body = builder.ToMessageBody();
            
            
                await client.SendAsync(emailMessage);
                
                //logger.Trace($"Сообщение отправлено, получатель '{count}' '{email}' из '{emailCount}'");
            }
            catch (Exception ex)
            {
                //logger.Error($"Произошла ошибка при отправке сообщения: {ex}");
                return false;
            }
            
            await client.DisconnectAsync(true);
            return true;
        }
    }
}
