namespace Meeting.Events.Infrastructure
{
    using Newtonsoft.Json;
    using System;

    public class EventAttendeeDocument
    {
        [JsonProperty("attendeeId")]
        public string AttendeeId { get; set; }

        [JsonProperty("joinedDate")]
        public DateTimeOffset JoinedDate { get; set; }
    }
}
