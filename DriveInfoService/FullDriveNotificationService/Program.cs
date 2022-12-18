using FullDriveNotificationService.Layers;
using FullDriveNotificationService.Models;

class Program
{
    static void Main()
    {
        string[] paths = new string[] { "C:\\" };
        double minPrecent = 50;

        LogicService logicService = new();

        List<FullDriveModel> fullDrivers = logicService.SearchFullDrivers(paths, minPrecent);
        (string subject, string message) = logicService.CreateSummaryMessage(fullDrivers);

        Console.WriteLine(subject);
        Console.WriteLine(message);
    }
}


