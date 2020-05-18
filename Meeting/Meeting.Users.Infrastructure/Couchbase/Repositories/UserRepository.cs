namespace Meeting.Users.Infrastructure
{
    using Couchbase;
    using Couchbase.Core;
    using Meeting.Users.Core;
    using System;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IBucket _usersBucket;

        public UserRepository(IUsersBucketProvider usersBucketProvider)
        {
            _usersBucket = usersBucketProvider.GetBucket();
        }

        public async Task CreateUser(string userId, CreateUserCommand createUserCommand)
        {
            var document = new Document<UserDocument>
            {
                Id = userId,
                Content = new UserDocument
                {
                    Name = createUserCommand.Name,
                    Email = createUserCommand.Email
                }
            };

            var documentResult = await _usersBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }
    }
}
