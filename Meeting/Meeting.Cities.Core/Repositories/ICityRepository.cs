namespace Meeting.Cities.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICityRepository
    {
        Task<bool> ExistsCity(Guid cityId);
        Task CreateCity(Guid cityId, CreateCityCommand createCityCommand);
        Task<List<GetCitiesByTermQueryResult>> GetCitiesByTerm(GetCitiesByTermQuery getCitiesByTermQuery);
    }
}
