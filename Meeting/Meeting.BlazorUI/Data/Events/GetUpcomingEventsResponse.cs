namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetUpcomingEventsResponse
    {
        public Guid EventId { get; set; }
        public string Subject { get; set; }
        public string UrlKey { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
