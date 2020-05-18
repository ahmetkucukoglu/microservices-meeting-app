namespace Meeting.Groups.Consumer
{
    using Couchbase;
    using Couchbase.Core;
    using MediatR;
    using Meeting.Groups.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserNotificationHandler : INotificationHandler<Notification>
    {
        private readonly IBucket _relationsBucket;

        public UserNotificationHandler(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            switch (notification.Event)
            {
                case Users.Core.V1.UserCreated x:
                    await OnUserCreated(x);
                    break;
            }
        }

        private async Task OnUserCreated(Users.Core.V1.UserCreated @event)
        {
            var document = new Document<UserDocument>
            {
                Id = $"users|{@event.UserId.ToString()}",
                Content = new UserDocument
                {
                    UserName = @event.UserName
                }
            };

            var documentResult = await _relationsBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }
    }
}
