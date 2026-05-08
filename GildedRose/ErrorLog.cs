using System;
using System.IO;

namespace GildedRoseKata
{
    public class ErrorLog
    {
        public static void LogException(Exception ex)
        {
            string errorMessage = $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}" +
                                  $"{ex.StackTrace}{Environment.NewLine}" +
                                  $"--------------------------------------------------{Environment.NewLine}";

            File.AppendAllText("error-log.txt", errorMessage);
        }
    }
}
