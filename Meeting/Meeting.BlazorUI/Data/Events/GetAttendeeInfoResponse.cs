namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetAttendeeInfoResponse
    {
        public bool AttendedIn { get; set; }
        public DateTimeOffset? JoinedDate { get; set; }
    }
}
