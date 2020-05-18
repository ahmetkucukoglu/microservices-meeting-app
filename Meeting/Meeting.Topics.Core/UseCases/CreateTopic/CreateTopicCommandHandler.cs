namespace Meeting.Topics.Core
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, Unit>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IEventStore _eventStore;

        public CreateTopicCommandHandler(ITopicRepository topicRepository, IEventStore eventStore)
        {
            _topicRepository = topicRepository;
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topicId = Guid.NewGuid();

            await _topicRepository.CreateTopic(topicId, request);

            await _eventStore.SaveAsync<V1.TopicCreated>(new V1.TopicCreated[] { new V1.TopicCreated(topicId, request.Name) });

            return Unit.Value;
        }
    }
}
