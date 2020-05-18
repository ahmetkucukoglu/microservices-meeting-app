namespace Meeting.Events.Infrastructure
{
    using Newtonsoft.Json;
    using System;

    public class EventCommentDocument
    {
        [JsonProperty("commentatorId")]
        public string CommentatorId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("commentedDate")]
        public DateTimeOffset CommentedDate { get; set; }
    }
}
