using FullDriveNotificationService.Layers;
using FullDriveNotificationService.Models;

class Program
{
    static void Main()
    {
        string[] paths = new string[] { "C:\\", "D:\\" };
        double minPrecent = 50;

        LogicService logicService = new();

        List<FullDriveModel> fullDrives = logicService.SearchFullDrives(paths, minPrecent);
        (string subject, string message) = logicService.CreateSummaryMessage(fullDrives);

        Console.WriteLine(subject);
        Console.WriteLine(message);
    }
}


