using System;
using System.IO;

namespace TwitchLurkerV2.Helpers
{
    class LogHandler
    {
        public static void Log(Log log)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string logDirectory = Path.Combine(appDataPath, @"Chastoca");
            logDirectory = Path.Combine(logDirectory, "LurkerV2");
            foreach (var item in log.FolderDirectory)
            {
                logDirectory = Path.Combine(logDirectory, item);
            }

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            if (log.LogName == null)
                log.LogName = "NO_LOG_NAME";

            var formattedLog = new Log();
            formattedLog.LogName = $"{log.LogName} - {DateTime.Now.ToShortDateString()}";
            formattedLog.Message = $"{DateTime.Now.ToLongTimeString()} || {log.Message} {Environment.NewLine}";

            string path = $"{logDirectory}\\{formattedLog.LogName}.txt";
            File.AppendAllText(path, formattedLog.Message);
            Console.WriteLine(log.Message);
        }

        public static void CrashReport(Exception ex)
        {
            var crashReport = new Log();
            crashReport.FolderDirectory = new string[] { "CrashReports" };
            crashReport.LogName = "CrashReport";
            crashReport.Message = string.Format("Exception message: {0}\nStack trace:\n{1}", ex.Message, ex.StackTrace);
            crashReport.TimeStamp = true;
            Log(crashReport);
        }
    }
}
