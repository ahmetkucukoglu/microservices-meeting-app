namespace Meeting.Cities.Core
{
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCitiesByTermQueryHandler : IRequestHandler<GetCitiesByTermQuery, IEnumerable<GetCitiesByTermQueryResult>>
    {
        private readonly ICityRepository _cityRepository;

        public GetCitiesByTermQueryHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<GetCitiesByTermQueryResult>> Handle(GetCitiesByTermQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetCitiesByTerm(request);

            return cities;
        }
    }
}
