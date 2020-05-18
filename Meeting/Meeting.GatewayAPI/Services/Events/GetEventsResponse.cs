namespace Meeting.GatewayAPI
{
    using System;

    public class GetEventsResponse
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
