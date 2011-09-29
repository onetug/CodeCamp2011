using System.Runtime.Serialization;

namespace CodeCamp.ASP.Web.UI
{
    using System;

    public class CodeCampAuthorizationException : Exception
    {
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