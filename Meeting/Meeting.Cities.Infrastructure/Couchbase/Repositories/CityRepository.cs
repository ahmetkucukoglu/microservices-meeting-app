namespace Meeting.Cities.Infrastructure
{
    using Couchbase;
    using Couchbase.Core;
    using Couchbase.Search;
    using Couchbase.Search.Queries.Simple;
    using Meeting.Cities.Core;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CityRepository : ICityRepository
    {
        private readonly IBucket _citiesBucket;

        public CityRepository(ICitiesBucketProvider citiesBucketProvider)
        {
            _citiesBucket = citiesBucketProvider.GetBucket();
        }

        public async Task<bool> ExistsCity(Guid cityId)
        {
            var isExists = await _citiesBucket.ExistsAsync(cityId.ToString());

            return isExists;
        }

        public async Task CreateCity(Guid cityId, CreateCityCommand createCityCommand)
        {
            var document = new Document<CityDocument>
            {
                Id = cityId.ToString(),
                Content = new CityDocument
                {
                    Name = createCityCommand.Name,
                    City = createCityCommand.City,
                    Country = createCityCommand.Country,
                    Location = new CityLocationDocument
                    {
                        Lat = createCityCommand.Latitude,
                        Lon = createCityCommand.Longitude
                    }
                }
            };

            var documentResult = await _citiesBucket.InsertAsync(document);

            if (!documentResult.Success)
                throw documentResult.Exception;
        }

        public async Task<List<GetCitiesByTermQueryResult>> GetCitiesByTerm(GetCitiesByTermQuery getCitiesByTermQuery)
        {
            var searchQuery = new SearchQuery
            {
                Index = "idx_cities",
                Query = new MatchQuery(getCitiesByTermQuery.Term).Fuzziness(1),
                SearchParams = new SearchParams().Limit(10).Timeout(TimeSpan.FromMilliseconds(10000))
            };

            searchQuery.Fields("city", "name", "location");

            var searchQueryResults = await _citiesBucket.QueryAsync(searchQuery);

            if (!searchQueryResults.Success)
                throw searchQueryResults.Exception;

            var result = new List<GetCitiesByTermQueryResult>();

            foreach (var hit in searchQueryResults.Hits)
            {
                result.Add(new GetCitiesByTermQueryResult
                {
                    Id = Guid.Parse(hit.Id),
                    Name = hit.Fields["name"],
                    Location = new GetCitiesByTermLocationQueryResult
                    {
                        Latitude = hit.Fields["location"][1],
                        Longitude = hit.Fields["location"][0]
                    }
                });
            }

            return result;
        }
    }
}
