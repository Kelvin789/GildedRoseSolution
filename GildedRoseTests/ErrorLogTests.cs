using GildedRoseKata;
using System;
using System.IO;
using Xunit;

namespace GildedRoseTests
{
    public class ErrorLogTests
    {
        #region Error Logging tests
        /// <summary>
        /// Deleting for simplicity, would append in production
        /// </summary>
        [Fact]
        public void LogError_Should_Write_Exception_Details_To_Log_File()
        {
            // Arrange
            const string logFilePath = "error-log.txt"; // Creates in ../GildedRoseTests/bin/Debug/net8.0/ during debug

            if (File.Exists(logFilePath))
                File.Delete(logFilePath);

            var exception = new Exception("Test exception message");

            // Act
            ErrorLog.LogException(exception);

            // Assert
            Assert.True(File.Exists(logFilePath));

            string logContents = File.ReadAllText(logFilePath);

            Assert.Contains("Test exception message", logContents);

            File.Delete(logFilePath);
        }
        #endregion
    }
}
