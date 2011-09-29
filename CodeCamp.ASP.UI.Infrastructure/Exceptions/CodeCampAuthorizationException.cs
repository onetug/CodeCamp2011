namespace CodeCamp.ASP.UI.Infrastructure
{
    using System;
    using System.Runtime.Serialization;

    public class CodeCampAuthorizationException : Exception
    {
        public string ProcessName { get; set; }

        public CodeCampAuthorizationException() : base()
        {
            
        }
        public CodeCampAuthorizationException(string message) : base(message)
        {

        }

        public CodeCampAuthorizationException(string message, Exception innerException) : base(message, innerException)
        {

        }

        protected CodeCampAuthorizationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            
        }
    }
}