namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetEventByIdResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string UrlKey { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int? Capacity { get; set; }
        public int AttendeeCount { get; set; }
        public string Status { get; set; }
        public GetEventByIdGroupResponse Group { get; set; }
        public GetEventByIdLocationResponse Location { get; set; }
    }

    public class GetEventByIdGroupResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class GetEventByIdLocationResponse
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
