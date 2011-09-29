namespace CodeCamp.ASP.Web.UI.Infrastructure
{
    using System;
    using CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts;

    public class LoggingService : ILoggingService
    {
        public LoggingService()
        {
            
        }
        
        public void LogMessage(string message)
        {
            
        }

        public void LogException(CodeCampAuthorizationException codeCampException)
        {
            //TODO JAS Do something with the log entry thingee
            var entry = new LogEntry(codeCampException.GetBaseException().Message);            
        }

        public void LogException(Exception codeCampException)
        {
            throw new NotImplementedException();
        }

        public void CreateLogEntry(LogEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}