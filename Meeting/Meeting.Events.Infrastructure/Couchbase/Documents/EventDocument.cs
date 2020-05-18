namespace Meeting.Events.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public class EventDocument
    {
        public EventDocument()
        {
            Attendees = new List<EventAttendeeDocument>();
            Comments = new List<EventCommentDocument>();
        }

        public Guid GroupId { get; set; }
        public string Subject { get; set; }
        public string UrlKey { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public int? Capacity { get; set; }
        public int AttendeeCount { get; set; }
        public EventLocationDocument Location { get; set; }
        public List<EventAttendeeDocument> Attendees { get; set; }
        public List<EventCommentDocument> Comments { get; set; }
    }
}
