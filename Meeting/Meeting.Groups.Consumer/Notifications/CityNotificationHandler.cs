namespace Meeting.Groups.Consumer
{
    using Couchbase;
    using Couchbase.Core;
    using MediatR;
    using Meeting.Groups.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class CityNotificationHandler : INotificationHandler<Notification>
    {
        private readonly IBucket _relationsBucket;

        public CityNotificationHandler(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            switch (notification.Event)
            {
                case Cities.Core.V1.CityCreated x:
                    await OnCityCreated(x);
                    break;
            }
        }

        private async Task OnCityCreated(Cities.Core.V1.CityCreated @event)
        {
            var document = new Document<CityDocument>
            {
                Id = $"cities|{@event.CityId.ToString()}",
                Content = new CityDocument
                {
                    CityName = @event.CityName
                }
            };

            var documentResult = await _relationsBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }
    }
}
