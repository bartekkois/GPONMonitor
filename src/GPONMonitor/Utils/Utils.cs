using System;

namespace GPONMonitor.Utils
{
    public static class TimeSpanConverter
    {
        public static string CalculateTimeSpanAndDateTimeFormSeconds(int? timeSpanInSeconds)
        {
            var calculatedTimeSpanAndDateTime = TimeSpan.FromSeconds(Convert.ToDouble(timeSpanInSeconds)).ToString(@"d\d\ h\:mm\:ss");

            if (timeSpanInSeconds != 0)
                calculatedTimeSpanAndDateTime = calculatedTimeSpanAndDateTime + " " + (DateTime.Now - TimeSpan.FromSeconds(Convert.ToDouble(timeSpanInSeconds))).ToString("(d.MM H:mm:ss)");

            return calculatedTimeSpanAndDateTime;
        }
    }
}
