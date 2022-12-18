using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullDriveNotificationService.Models
{
    public record class FullDriveModel(string DriveName, double AvailableFreeSpaceGb, double TotalSizeSpaceGb, double Percent);
}
