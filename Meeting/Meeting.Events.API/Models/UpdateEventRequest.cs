namespace Meeting.Events.API
{
    using System;

    public class UpdateEventRequest
    {
        public string Subject { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
