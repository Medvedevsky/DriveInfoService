using FullDriveNotificationService.Logic;
using FullDriveNotificationService.Models;
using NLog;
using System.Text;

namespace FullDriveNotificationService.Layers
{
    public static class FullDriveNotification
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static List<FullDriveModel> SearchFullDrives(string[] paths, double minPrecent)
        {
            logger.Info("Запустилась проверка свободного места на дисках");

            if (paths is null)
            {
                logger.Error("Произошла ошибка. Путь к диску не указан!");
                return null!;
            }
            List<FullDriveModel> fullDrives = new List<FullDriveModel>();

            foreach (var path in paths)
            {
                bool success = Kernel32.GetDiskFreeSpaceEx($@"{path}",
                        out ulong availableFreeSpace,
                        out ulong totalSize,
                        out ulong totalFreesSpace);

                if (!success)
                {
                    logger.Error($"Произошла ошибка. Не удалось получить информацию о диске {path}");
                    return null!;
                }

                double availableFreeSpaceGb = ConvertByteToGigabyte(availableFreeSpace);
                double totalSizeSpaceGb = ConvertByteToGigabyte(totalSize);
                double precent = GetPercent(totalSizeSpaceGb, availableFreeSpaceGb);

                if (minPrecent >= precent)
                {
                    fullDrives.Add(
                        new FullDriveModel(
                            path,
                            availableFreeSpaceGb,
                            totalSizeSpaceGb,
                            precent));
                }
            }

            double ConvertByteToGigabyte(ulong value)
            {
                //gigabyte = 1073741824;
                return value / 1073741824;
            }

            double GetPercent(double totalSizeGb, double availableFreeSpaceGb)
            {
                return Convert.ToDouble(string.Format("{0:F2}", (double)availableFreeSpaceGb * 100 / totalSizeGb));
            }

            logger.Info("Завершилась проверка свободного места на дисках");
            return fullDrives;
        }
        public static (string subject, string message) CreateSummaryMessage(List<FullDriveModel> drives)
        {
            StringBuilder builder = new StringBuilder();
            //string subject = "Проблема";
            string subject = "Problem";

            //builder.AppendLine("Заканчивается свободное место");
            builder.AppendLine("Free memory is running out");

            foreach (var fullDrive in drives)
            {
                //builder.AppendLine($"Расположение {fullDrive.DriveName} - {fullDrive.AvailableFreeSpaceGb} ГБ " +
                //    $"из {fullDrive.TotalSizeSpaceGb} ГБ ({fullDrive.Percent} %)");

                builder.AppendLine($"Location {fullDrive.DriveName} - {fullDrive.AvailableFreeSpaceGb} GB " +
                      $"out of {fullDrive.TotalSizeSpaceGb} GB ({fullDrive.Percent} %)");
            }

            return (subject, builder.ToString());

        }
    }
}
