namespace CodeCamp.ASP.UI.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Web;
    using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
    using Microsoft.Practices.EnterpriseLibrary.Logging;

    public class LoggingService : ILoggingService
    {
        public LoggingService() {}
        
        public void LogMessage(string infoMessage)
        {
            LogEntry entry = InitializeLogEntry();
            entry.Message = infoMessage;
            entry.Severity = TraceEventType.Information;
            Logger.Write(entry);
        }

        public void LogWarning(string warningMessage)
        {
            LogEntry entry = InitializeLogEntry();
            entry.Message = warningMessage;
            entry.Severity = TraceEventType.Warning;
            Logger.Write(entry);
        }

        public void LogException(CodeCampAuthorizationException codeCampException)
        {
            var entry = InitializeLogEntry();
            entry.Message = codeCampException.GetBaseException().Message;

            entry.Severity = TraceEventType.Error;
            entry.ProcessName = codeCampException.ProcessName;

            Logger.Write(entry);
        }
        public void LogException(CodeCampConfigurationException codeCampException)
        {
            var entry = InitializeLogEntry();
            entry.Message = codeCampException.GetBaseException().Message;

            entry.Severity = TraceEventType.Error;
            entry.ProcessName = codeCampException.ProcessName;
            entry.ExtendedProperties.Add("ConfigItem", codeCampException.ConfigurationItem);
            Logger.Write(entry);
        }


        public void LogException(Exception unhandledException)
        {
            var entry = InitializeLogEntry();

            entry.Message = unhandledException.GetBaseException().Message;
            entry.ProcessName = "Unhandled exception.";

            Logger.Write(entry);
        }

        private LogEntry InitializeLogEntry()
        {
            var entry = new LogEntry
            {
                MachineName = HttpContext.Current.Server.MachineName,
                TimeStamp = DateTime.Now
            };
            return entry;
        }
    }
}