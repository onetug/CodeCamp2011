namespace CodeCamp.RIA.UI.Events
{
    using System;

    public class ErrorWindowEvent
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public string ViewModelName { get; set; }
    }
}
