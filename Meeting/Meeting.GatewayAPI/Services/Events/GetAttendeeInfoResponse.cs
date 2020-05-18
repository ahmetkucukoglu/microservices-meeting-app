namespace Meeting.GatewayAPI
{
    using System;

    public class GetAttendeeInfoResponse
    {
        public bool AttendedIn { get; set; }
        public DateTimeOffset? JoinedDate { get; set; }
    }
}
