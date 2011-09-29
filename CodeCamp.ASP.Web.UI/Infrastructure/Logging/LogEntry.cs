namespace CodeCamp.ASP.Web.UI.Infrastructure
{
    public class LogEntry
    {
        public long? Id { get; private set; }

        public string Message { get; private set; }

        public LogEntry(long id, string message)
        {
            this.Message = message;
            this.Id = id;
        }
        
        public LogEntry(string message)
        {
            this.Message = message;
            this.Id = null;
        }
    }
}