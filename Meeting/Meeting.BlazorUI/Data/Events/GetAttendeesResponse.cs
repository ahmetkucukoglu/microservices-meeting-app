namespace Meeting.BlazorUI.Data
{
    using System;

    public class GetAttendeesResponse
    {
        public string AttendeeId { get; set; }
        public string AttendeeName { get; set; }
        public DateTimeOffset JoinedDate { get; set; }
    }
}
