namespace CodeCamp.ASP.UI.Infrastructure.ServiceContracts
{
    using System;

    public interface ILoggingService
    {
        void LogMessage(string message);

        void LogWarning(string warningMessage);

        void LogException(CodeCampAuthorizationException codeCampException);

        void LogException(Exception codeCampException);
    }
}
