namespace Meeting.Topics.Infrastructure
{
    using Couchbase;
    using Couchbase.Core;
    using Couchbase.Search;
    using Couchbase.Search.Queries.Simple;
    using Meeting.Topics.Core;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TopicRepository : ITopicRepository
    {
        private readonly IBucket _topicsBucket;

        public TopicRepository(ITopicsBucketProvider topicsBucketProvider)
        {
            _topicsBucket = topicsBucketProvider.GetBucket();
        }

        public async Task<bool> ExistsTopic(Guid topicId)
        {
            var isExists = await _topicsBucket.ExistsAsync(topicId.ToString());

            return isExists;
        }

        public async Task CreateTopic(Guid topicId, CreateTopicCommand createTopicCommand)
        {
            var document = new Document<TopicDocument>
            {
                Id = topicId.ToString(),
                Content = new TopicDocument
                {
                    Name = createTopicCommand.Name,
                    Description = createTopicCommand.Description
                }
            };

            var documentResult = await _topicsBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        public async Task<List<GetTopicsByTermQueryResult>> GetTopicsByTerm(GetTopicsByTermQuery getTopicsByTermQuery)
        {
            var searchQuery = new SearchQuery
            {
                Index = "idx_topics",
                Query = new MatchQuery(getTopicsByTermQuery.Term).Fuzziness(1),
                SearchParams = new SearchParams().Limit(10).Timeout(TimeSpan.FromMilliseconds(10000))
            };

            searchQuery.Fields("name");

            var searchQueryResults = await _topicsBucket.QueryAsync(searchQuery);

            if (!searchQueryResults.Success)
                throw searchQueryResults.Exception;

            var result = new List<GetTopicsByTermQueryResult>();

            foreach (var hit in searchQueryResults.Hits)
            {
                result.Add(new GetTopicsByTermQueryResult
                {
                    Id = Guid.Parse(hit.Id),
                    Name = hit.Fields["name"]
                });
            }

            return result;
        }
    }
}
