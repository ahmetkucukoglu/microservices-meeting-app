namespace Meeting.Topics.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITopicRepository
    {
        Task<bool> ExistsTopic(Guid topicId);
        Task CreateTopic(Guid topicId, CreateTopicCommand createTopicCommand);
        Task<List<GetTopicsByTermQueryResult>> GetTopicsByTerm(GetTopicsByTermQuery getTopicsByTermQuery);
    }
}
