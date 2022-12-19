using FullDriveNotificationService.Layers;
using FullDriveNotificationService.Models;
using MailService.Models;
using MailService.Infrastructure;

class Program
{
    static async Task Main()
    {
        string[] paths = new string[] { "C:\\", "D:\\" };
        double minPrecent = 50;

        LogicService logicService = new();

        List<FullDriveModel> fullDrives = logicService.SearchFullDrives(paths, minPrecent);
        (string subject, string message) = logicService.CreateSummaryMessage(fullDrives);

        SendingSettings settings = new SendingSettings();
        settings.Subject = subject;
        settings.Message = message;
        settings.SenderHost = "smtp.mailtrap.io";
        settings.SenderPort = 2525;
        settings.UserName = "7320d25e4ed208";
        settings.Password = "9e307a2f723aa1";
        settings.SenderEmail = "MailService@gmail.com";
        settings.Sender = "TestMailService";

        string recepientEmail = "killumexxx@gmail.com";

        await SendService.SendEmailAsync(settings, recepientEmail);

        Console.WriteLine(subject);
        Console.WriteLine(message);
    }
}


