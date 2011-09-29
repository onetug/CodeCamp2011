namespace CodeCamp.ASP.UI.Infrastructure
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class CodeCampCacheException : Exception
    {
        public CodeCampCacheException()
        {
        }

        public CodeCampCacheException(string message) : base(message)
        {
        }

        public CodeCampCacheException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CodeCampCacheException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}