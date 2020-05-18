namespace Meeting.Events.Core
{
    using System;

    public class GetAttendeesQueryResult
    {
        public string AttendeeId { get; set; }
        public string AttendeeName { get; set; }
        public DateTimeOffset JoinedDate { get; set; }
    }
}
