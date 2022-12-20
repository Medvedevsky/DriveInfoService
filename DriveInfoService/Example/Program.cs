using FullDriveNotificationService.Layers;
using FullDriveNotificationService.Models;
using MailService.Infrastructure;
using MailService.Models;

class Program
{
    static async Task Main()
    {
        string[] paths = new string[] { "C:\\", "D:\\" };
        double minPrecent = 50;

        FullDriveNotification logicService = new();

        List<FullDriveModel> fullDrives = logicService.SearchFullDrives(paths, minPrecent);
        (string subject, string message) = logicService.CreateSummaryMessage(fullDrives);

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

        string recepientEmail = "killumexxx@gmail.com";

        await SendService.SendEmailAsync(settings, recepientEmail);

        Console.WriteLine();
        Console.WriteLine(subject);
        Console.WriteLine(message);
    }
}


