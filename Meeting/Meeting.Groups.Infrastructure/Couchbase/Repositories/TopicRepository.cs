namespace Meeting.Groups.Infrastructure
{
    using Couchbase.Core;
    using Meeting.Groups.Core;
    using System;
    using System.Threading.Tasks;

    public class TopicRepository : ITopicRepository
    {
        private readonly IBucket _relationsBucket;

        public TopicRepository(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task<bool> ExistsTopic(Guid topicId)
        {
            var isExists = await _relationsBucket.ExistsAsync($"topics|{topicId.ToString()}");

            return isExists;
        }
    }
}
