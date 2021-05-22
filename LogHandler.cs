using System;
using System.IO;

namespace TwitchLurkerV2
{
    class LogHandler
    {
        public static void Log(Log log)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string logDirectory = Path.Combine(appDataPath, @"Chastoca");
            logDirectory = Path.Combine(logDirectory, "LurkerV2");
            logDirectory = Path.Combine(logDirectory, log.LogName);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            if (log.LogName == null)
                log.LogName = "NO_LOG_NAME";

            var formattedLog = new Log
            {
                LogName = string.Format(" {0} - {1}", log.LogName, DateTime.Today.ToString().Substring(0, 10)),
                Message = log.Message
            };

            string path = logDirectory + "\\" + formattedLog.LogName + ".txt";
            File.AppendAllText(path, LogToFile(formattedLog));
            LogToConsole(formattedLog);
        }
      
        public static string LogToFile(Log log)
        {
            string logMessage = String.Format("{0} >>>> {1} {2}  ",
                DateTime.Now,
                log.Message,
                Environment.NewLine);

            return logMessage;
        }

        public static void LogToConsole(Log log)
        {
            string logMessage = String.Format("{0} >>>>  {1}  ",
                DateTime.Now,
                log.Message);
            Console.WriteLine(logMessage);
        }

        public static void CrashReport(Exception ex)
        {
            Log crashReport = new Log
            {
                LogName = "CrashReports",
                Message = string.Format("Exception message: {0}\nStack trace:\n{1}", ex.Message, ex.StackTrace)
            };
            Log(crashReport);
        }

    }
}
