namespace Meeting.Topics.Core
{
    using System;

    public partial class V1
    {
        public class TopicCreated
        {
            public Guid TopicId { get; set; }
            public string TopicName { get; set; }
        }
    }
}
