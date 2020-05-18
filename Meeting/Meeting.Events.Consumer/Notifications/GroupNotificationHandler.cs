namespace Meeting.Events.Consumer
{
    using Couchbase;
    using Couchbase.Core;
    using MediatR;
    using Meeting.Events.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class GroupNotificationHandler : INotificationHandler<Notification>
    {
        private readonly IBucket _relationsBucket;

        public GroupNotificationHandler(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            switch (notification.Event)
            {
                case Groups.Core.V1.GroupCreated x:
                    await OnGroupCreated(x);
                    break;
            }
        }

        private async Task OnGroupCreated(Groups.Core.V1.GroupCreated @event)
        {
            var document = new Document<GroupDocument>
            {
                Id = $"groups|{@event.GroupId.ToString()}",
                Content = new GroupDocument
                {
                    GroupName = @event.GroupName,
                    OrganizerId = @event.OrganizerId
                }
            };

            var documentResult = await _relationsBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }
    }
}
