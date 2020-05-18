namespace Meeting.Cities.Core
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Unit>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IEventStore _eventStore;

        public CreateCityCommandHandler(ICityRepository cityRepository, IEventStore eventStore)
        {
            _cityRepository = cityRepository;
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var cityId = Guid.NewGuid();

            await _cityRepository.CreateCity(cityId, request);

            await _eventStore.SaveAsync<V1.CityCreated>(new V1.CityCreated[] { new V1.CityCreated(cityId, request.Name) });

            return Unit.Value;
        }
    }
}
