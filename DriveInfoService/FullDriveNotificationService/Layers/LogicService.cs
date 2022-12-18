using FullDriveNotificationService.Models;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace FullDriveNotificationService.Layers
{
    public class LogicService
    {
        public (string subject, string message) CreateSummaryMessage(List<FullDriveModel> drivers)
        {
            StringBuilder builder = new StringBuilder();
            string subject = "Проблема";

            builder.AppendLine("Заканчивается свободное место");

            foreach (var fullDrive in drivers)
            {
                builder.AppendLine($"Расположение {fullDrive.DriveName} - {fullDrive.AvailableFreeSpaceGb} ГБ " +
                    $"из {fullDrive.TotalSizeSpaceGb} ГБ ({fullDrive.Percent} %)");
            }

            return (subject, builder.ToString());

        }

        public List<FullDriveModel> SearchFullDrivers(string[] paths, double minPrecent)
        {

            if (paths is null) return null!;
            List<FullDriveModel> fullDrivers = new List<FullDriveModel>();

            foreach (var path in paths)
            {
                bool success = Kernel32.GetDiskFreeSpaceEx($@"{path}",
                        out ulong availableFreeSpace,
                        out ulong totalSize,
                        out ulong totalFreesSpace);

                if (!success)
                {
                    Console.WriteLine("Не удалось получить информацию о диске");
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                double availableFreeSpaceGb = ConvertByteToGigabyte(availableFreeSpace);
                double totalSizeSpaceGb = ConvertByteToGigabyte(totalSize);
                double precent = GetPercent(totalSizeSpaceGb, availableFreeSpaceGb);

                if (minPrecent >= precent)
                {
                    fullDrivers.Add(
                        new FullDriveModel(
                            path,
                            availableFreeSpaceGb,
                            totalSizeSpaceGb,
                            precent));
                }
            }

            return fullDrivers;
        }

        public double ConvertByteToGigabyte(ulong value)
        {
            //gigabyte = 1073741824;
            return value / 1073741824;
        }

        public double GetPercent(double totalSizeGb, double availableFreeSpaceGb)
        {
            return Convert.ToDouble(string.Format("{0:F2}", (double)availableFreeSpaceGb * 100 / totalSizeGb));
        }
    }
}
