namespace Meeting.Events.Core
{
    using System;

    public class GetAttendeeInfoQueryResult
    {
        public bool AttendedIn => JoinedDate != null;
        public DateTimeOffset? JoinedDate { get; set; }
    }
}
