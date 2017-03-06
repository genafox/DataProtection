using System;
using System.IO;
using Contracts.Interfaces;

namespace Logs.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly string logFilePath;

        public FileLogger(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void LogInfo(string message)
        {
            string log = $"{Environment.NewLine}[INFO] - {message}";
            File.AppendAllText(this.logFilePath, log);
        }
    }
}
