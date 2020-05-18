namespace Meeting.Events.Core
{
    using System;

    public class GetEventsQueryResult
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? Capacity { get; set; }
        public int AttendeeCount { get; set; }
        public string Status { get; set; }
    }
}
