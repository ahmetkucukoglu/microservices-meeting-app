namespace Meeting.Events.API
{
    using System;

    public class GetGroupEventsRequest
    {
        public Guid GroupId { get; set; }
        public string Status { get; set; }
    }
}
