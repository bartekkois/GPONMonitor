using System;
using System.Security.Cryptography;
using System.Text;

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

    public static class HashCalculator
    {
        public static string CalculateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("X2"));
            return sb.ToString().ToLower();
        }
    }
}