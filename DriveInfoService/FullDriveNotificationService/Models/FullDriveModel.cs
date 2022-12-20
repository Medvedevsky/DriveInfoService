namespace FullDriveNotificationService.Models
{
    public record class FullDriveModel(
        string DriveName,
        double AvailableFreeSpaceGb,
        double TotalSizeSpaceGb,
        double Percent);
}
