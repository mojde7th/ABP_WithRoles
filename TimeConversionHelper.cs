using System;

namespace TodoApp.Helpers
{
    public static class TimeConversionHelper
{
    public static string
        ConvertMinutesToHoursAndMinutes(int
          totalMinutes)
    {
        int hours = totalMinutes / 60;
        int remainingMinutes = totalMinutes % 60;
        return $"{hours}.{remainingMinutes:D2}";
    }
}
}