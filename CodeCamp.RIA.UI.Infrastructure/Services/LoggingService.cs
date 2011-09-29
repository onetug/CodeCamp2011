namespace CodeCamp.RIA.UI.Infrastructure.Services
{
    using System;
    using System.ServiceModel.DomainServices.Client;
    using CodeCamp.RIA.Data.Web;

    public class LoggingService : ILoggingService
    {
        private CodeCampDomainContext context = new CodeCampDomainContext();

        public LoggingService() { }

        public virtual void LogMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            Log entry = InitializeLogEntry();
            entry.Message = message;
            entry.Severity = "Infomation";
            WriteLog(entry);
        }

        public virtual void LogWarning(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            Log entry = InitializeLogEntry();
            entry.Message = message;
            entry.Severity = "Warning";
            WriteLog(entry);
        }

        public virtual void LogException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            Log entry = InitializeLogEntry();
            entry.Severity = "Error";
            entry.Message = ex.GetBaseException().Message;
            WriteLog(entry);
        }

        private static Log InitializeLogEntry()
        {
            var entry = new Log
                            {
                                Title = string.Empty,
                                Timestamp = DateTime.Now,
                                AppDomainName = AppDomain.CurrentDomain.FriendlyName,
                                EventID = 0,
                                Priority = -1,
                                MachineName = string.Empty
                            };
            return entry;
        }

        private void WriteLog(Log entry)
        {
            try
            {
                context.Logs.Add(entry);
                context.SubmitChanges(ChangesSubmitted, null);   
            }

            catch(Exception ex)
            {
                string s = ex.GetBaseException().Message;
            }
            
        }

        private void ChangesSubmitted(SubmitOperation submitOperation)
        {
            if(submitOperation.Error != null)
            {
                // Gulp! JAS
                // throw submitOperation.Error;
            }
        }

    }
}
