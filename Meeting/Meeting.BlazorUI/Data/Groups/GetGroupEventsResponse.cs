namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetGroupEventsResponse
    {
        public Guid EventId { get; set; }
        public string Subject { get; set; }
        public string UrlKey { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTimeOffset Date { get; set; }
        public int AttendeeCount { get; set; }
    }
}
