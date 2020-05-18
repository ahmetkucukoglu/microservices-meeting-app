namespace Meeting.Events.Infrastructure
{
    using Couchbase.Core;
    using Meeting.Events.Core;
    using System;
    using System.Threading.Tasks;

    public class GroupRepository : IGroupRepository
    {
        private readonly IBucket _relationsBucket;

        public GroupRepository(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task<bool> ExistsGroup(Guid groupId)
        {
            var isExists = await _relationsBucket.ExistsAsync($"groups|{groupId.ToString()}");

            return isExists;
        }

        public async Task<bool> ExistsGroup(Guid groupId, string organizerId)
        {
            var documentResult = await _relationsBucket.GetDocumentAsync<GroupDocument>($"groups|{groupId.ToString()}");

            return documentResult.Content != null && documentResult.Content.OrganizerId == organizerId;
        }
    }
}
