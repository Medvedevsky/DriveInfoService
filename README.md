# DriveInfoService

## Description
### Service use Pinvoke(kernel32) dll 

https://www.pinvoke.net/default.aspx/kernel32.GetDiskFreeSpaceEx

```c#
    internal static class Kernel32
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
                                       out ulong lpFreeBytesAvailable,
                                       out ulong lpTotalNumberOfBytes,
                                       out ulong lpTotalNumberOfFreeBytes);
    }
 ```   
GetDiskFreeSpaceEx - the method of searching for free disk space on the path (return empty memorys in bytes)

## Usage
#### For use you must specify path to your drive and the minimal percentage of empty space for notification
Example.cs
```c#
class Program
{
    static async Task Main()
    {
        // can UNC paths also, example \\Server1\c$\
        string[] paths = new string[] { "D:\\", "C:\\" };
        double minPercent = 50;

        List<FullDriveModel> fullDrives = FullDriveNotification.SearchFullDrives(paths, minPrecent);
        (string subject, string message) = FullDriveNotification.CreateSummaryMessage(fullDrives);
        
        string recepientEmail = "RecepientEmail@gmail.com";
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
        
        await SendService.SendEmailAsync(settings, recepientEmail);
    }
}
```

### Example result on email

<img src=https://github.com/Medvedevsky/DriveInfoService/blob/draft-dll-branch/DriveInfoService/Example/Images/ExampleResult.png>




