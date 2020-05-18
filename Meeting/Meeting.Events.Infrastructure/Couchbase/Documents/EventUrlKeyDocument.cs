namespace Meeting.Events.Infrastructure
{
    using System;

    public class EventUrlKeyDocument
    {
        public string UrlKey { get; set; }
        public Guid EventId { get; set; }
    }
}
