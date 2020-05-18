namespace Meeting.Groups.Consumer
{
    using Couchbase;
    using Couchbase.Core;
    using MediatR;
    using Meeting.Groups.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class TopicNotificationHandler : INotificationHandler<Notification>
    {
        private readonly IBucket _relationsBucket;

        public TopicNotificationHandler(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            switch (notification.Event)
            {
                case Topics.Core.V1.TopicCreated x:
                    await OnCityCreated(x);
                    break;
            }
        }

        private async Task OnCityCreated(Topics.Core.V1.TopicCreated @event)
        {
            var document = new Document<TopicDocument>
            {
                Id = $"topics|{@event.TopicId.ToString()}",
                Content = new TopicDocument
                {
                    TopicName = @event.TopicName
                }
            };

            var documentResult = await _relationsBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }
    }
}
