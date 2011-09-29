namespace CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts
{
    using System;

    public interface ILoggingService
    {
        void LogMessage(string message);

        void LogException(CodeCampAuthorizationException codeCampException);

        void LogException(Exception codeCampException);

        void CreateLogEntry(LogEntry entry);
    }
}
