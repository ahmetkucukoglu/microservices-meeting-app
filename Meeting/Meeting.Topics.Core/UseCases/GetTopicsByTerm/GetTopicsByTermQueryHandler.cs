namespace Meeting.Topics.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetTopicsByTermQueryHandler : IRequestHandler<GetTopicsByTermQuery, IEnumerable<GetTopicsByTermQueryResult>>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicsByTermQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<IEnumerable<GetTopicsByTermQueryResult>> Handle(GetTopicsByTermQuery request, CancellationToken cancellationToken)
        {
            var topics = await _topicRepository.GetTopicsByTerm(request);

            return topics;
        }
    }
}
