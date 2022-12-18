// See https://aka.ms/new-console-template for more information
using testApp;

Kernel32.GetDiskFreeSpaceEx("C:\\", out ulong lpFreeBytesAvailable,
                                    out ulong lpTotalNumberOfBytes,
                                    out ulong lpTotalNumberOfFreeBytes);

var avalivaleGb = lpFreeBytesAvailable / 1073741824;
var totalNumberGb = lpTotalNumberOfBytes / 1073741824;
var totalNumberOfFreeBytes = lpTotalNumberOfFreeBytes / 1073741824;

Console.WriteLine($"Доступно: {avalivaleGb}");
Console.WriteLine($"Всего: {totalNumberGb}");

