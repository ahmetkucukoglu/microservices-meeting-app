namespace Meeting.Groups.Infrastructure
{
    using Couchbase.Core;
    using Meeting.Groups.Core;
    using System;
    using System.Threading.Tasks;

    public class CityRepository : ICityRepository
    {
        private readonly IBucket _relationsBucket;

        public CityRepository(IRelationsBucketProvider relationsBucketProvider)
        {
            _relationsBucket = relationsBucketProvider.GetBucket();
        }

        public async Task<bool> ExistsCity(Guid cityId)
        {
            var isExists = await _relationsBucket.ExistsAsync($"cities|{cityId.ToString()}");

            return isExists;
        }
    }
}
