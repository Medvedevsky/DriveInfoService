using FullDriveNotificationService.Layers;
using FullDriveNotificationService.Models;
using MailService.Infrastructure;
using MailService.Models;

class Program
{
    static async Task Main()
    {
        string[] paths = new string[] { "D:\\", "C:\\" };
        double minPrecent = 50;

        List<FullDriveModel> fullDrives = FullDriveNotification.SearchFullDrives(paths, minPrecent);
        (string subject, string message) = FullDriveNotification.CreateSummaryMessage(fullDrives);

        SendingSettings settings = new SendingSettings
        {
           Subject = subject,
           Message = message,
           SenderHost = "smtp.mailtrap.io",
           SenderPort = 2525,
           UserName = "7320d25e4ed208",
           Password = "9e307a2f723aa1",
           SenderEmail = "MailService@gmail.com",
           Sender = "TestMailService",
        };

        string recepientEmail = "recepientEmail@gmail.com";

        await SendService.SendEmailAsync(settings, recepientEmail);
    }
}


