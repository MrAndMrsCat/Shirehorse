using Shirehorse.Core.Diagnostics.Logging;
using Shirehorse.Core.Extensions;
using System.Security.Principal;
using System.Diagnostics;

namespace Shirehorse.Core.Diagnostics
{
    public static class DiagnositicPolicy
    {
        public static bool IsDeveloper => Environment.UserName == "messeal" || Environment.UserName == "ajmes";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Project only for windows delpoyment")]
        public static bool ApplicationIsRunningAsAdmin => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        static DiagnositicPolicy()
        {
            if (IsDeveloper)
            {
                EnableExceptionTextLog();
                EnableEventTextLog();
                EnableDebugTextLog();
            }
        }

        public static void ElevateApplicationToAdmin()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas"
                });

                Application.Exit();
            }
            catch (Exception ex)
            {
                SystemHandler.Handle(ex);
            }
        }

        public static void EnableConsole()
        {
            SystemLog.EnableConsoleLog();
            SystemLog.ConsoleLog?.Show();
            SystemLog.Log("Enabled console");
        }

        public static void DisableConsole()
        {
            SystemLog.ConsoleLog?.Hide();
            SystemLog.ConsoleLog = null;
            SystemLog.Log("Disabled console");
        }

        public static void EnableExceptionWindow()
        {
            if (SystemHandler.Handler is not ExceptionForm)
            {
                SystemHandler.Handler = new ExceptionForm();
                SystemLog.Log("Enabled exception dialog forms");
            }
        }

        public static void DisableExceptionWindow()
        {
            if (SystemHandler.Handler is ExceptionForm)
            {
                SystemHandler.Handler = null;
                SystemLog.Log("Disabled exception dialog forms");
            }
        }

        public static void EnableExceptionTextLog()
        {
            if (!_exceptionLogEnabled)
            {

                CreateTextLog(new()
                {
                    LogFileName = "Exception Log",
                    TextLogDirectoryPath = @"c:\temp\debug\ExceptionLogs",
                    Level = ILogging.Category.Exception,
                    Fields = ILogging.Fields.Timestamp | ILogging.Fields.Source
                });

                _exceptionLogEnabled = true;
            }
        }

        private static bool _exceptionLogEnabled = false;

        public static void EnableEventTextLog()
        {
            if (!_eventLogEnabled)
            {
                CreateTextLog(new()
                {
                    LogFileName = "Event Log",
                    TextLogDirectoryPath = @"c:\temp\debug\EventLogs",
                    Level = ILogging.Category.Information,
                    Fields = ILogging.Fields.All
                });

                _eventLogEnabled = true;
            }
        }
        private static bool _eventLogEnabled = false;

        public static void EnableDebugTextLog()
        {
            if (!_debugLogEnabled)
            {
                CreateTextLog(new()
                {
                    LogFileName = "Debug Log",
                    TextLogDirectoryPath = @"c:\temp\debug\DebugLogs",
                    Level = ILogging.Category.Debug,
                    Fields = ILogging.Fields.All
                });

                _debugLogEnabled = true;
            }
        }
        private static bool _debugLogEnabled = false;

        private static void CreateTextLog(TextFileLog textLog)
        {
            Application.ApplicationExit += (s, e) => textLog.Flush();

            SystemLog.LogSinks.Add(textLog);

            SystemLog.Log($"{textLog.LogFileName} enabled in directory: {textLog.TextLogDirectoryPath}");
        }

    }
    
}
