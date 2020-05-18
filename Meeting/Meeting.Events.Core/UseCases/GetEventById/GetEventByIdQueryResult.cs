namespace Meeting.Events.Core
{
    using System;

    public class GetEventByIdQueryResult
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string UrlKey { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int? Capacity { get; set; }
        public int AttendeeCount { get; set; }
        public string Status { get; set; }
        public int CommentsCount { get; set; }
        public GetEventByIdGroupQueryResult Group { get; set; }
        public GetEventByIdLocationQueryResult Location { get; set; }
    }

    public class GetEventByIdGroupQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OrganizerId { get; set; }
    }

    public class GetEventByIdLocationQueryResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
