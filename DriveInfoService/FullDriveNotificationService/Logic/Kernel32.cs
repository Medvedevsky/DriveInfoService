using System.Runtime.InteropServices;

namespace FullDriveNotificationService.Logic
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
                                       out ulong lpFreeBytesAvailable,
                                       out ulong lpTotalNumberOfBytes,
                                       out ulong lpTotalNumberOfFreeBytes);
    }
}
