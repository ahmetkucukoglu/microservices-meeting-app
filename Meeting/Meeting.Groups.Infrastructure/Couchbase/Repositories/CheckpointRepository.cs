namespace Meeting.Groups.Infrastructure
{
    using Couchbase;
    using Couchbase.Core;
    using global::EventStore.ClientAPI;
    using System.Threading.Tasks;

    public class CheckpointRepository
    {
        private readonly IBucket _relationsBucket;

        public CheckpointRepository(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task<Position?> GetAsync(string key)
        {
            var result = await _relationsBucket.GetAsync<CheckpointDocument>($"checkpoints|{key}");

            if (result.Value == null)
                return null;

            return result.Value.Position;
        }

        public async Task<bool> SaveAsync(string key, Position position)
        {
            var doc = new Document<CheckpointDocument>
            {
                Id = $"checkpoints|{key}",
                Content = new CheckpointDocument
                {
                    Key = $"checkpoints|{key}",
                    Position = position
                }
            };

            var result = await _relationsBucket.UpsertAsync(doc);

            return result.Success;
        }
    }
}
