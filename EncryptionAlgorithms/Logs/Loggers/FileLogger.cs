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

        public void LogInfo(params string[] messages)
        {
            string log = $"{Environment.NewLine}[INFO] - {string.Join("\r\n", messages)}";
            File.AppendAllText(this.logFilePath, log);
        }
    }
}
