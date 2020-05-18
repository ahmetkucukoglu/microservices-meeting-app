namespace Meeting.Topics.Core
{
    using System;

    public partial class V1
    {
        public class TopicCreated : Event
        {
            public TopicCreated(Guid topicId, string topicName)
            {
                TopicId = topicId;
                TopicName = topicName;
            }

            public Guid TopicId { get; }
            public string TopicName { get; }
        }
    }
}
