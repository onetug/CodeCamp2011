namespace CodeCamp.ASP.UI.Infrastructure
{
    using System;
    using System.Runtime.Serialization;

    public class CodeCampConfigurationException : Exception
    {
        public string ProcessName { get; set; }
        public string ConfigurationItem { get; set; }

        public CodeCampConfigurationException()
            : base()
        {

        }
        public CodeCampConfigurationException(string message)
            : base(message)
        {

        }

        public CodeCampConfigurationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        protected CodeCampConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}