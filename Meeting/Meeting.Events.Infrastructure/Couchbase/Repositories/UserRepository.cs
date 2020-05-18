namespace Meeting.Events.Infrastructure
{
    using Couchbase.Core;
    using Meeting.Events.Core;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IBucket _relationsBucket;

        public UserRepository(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task<bool> ExistsUser(string userId)
        {
            var isExists = await _relationsBucket.ExistsAsync($"users|{userId.ToString()}");

            return isExists;
        }
    }
}
