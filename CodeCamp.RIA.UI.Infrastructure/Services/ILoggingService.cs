using System;
using System.ComponentModel;

namespace CodeCamp.RIA.UI.Infrastructure.Services
{
    public interface ILoggingService
    {
        void LogMessage(string message);
        void LogWarning(string message);
        void LogException(Exception ex);
    }
}