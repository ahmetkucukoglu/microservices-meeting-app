namespace Meeting.Groups.Core
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Unit>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventStore _eventStore;

        public CreateGroupCommandHandler(IGroupRepository groupRepository, ICityRepository cityRepository, ITopicRepository topicRepository, IUserRepository userRepository, IEventStore eventStore)
        {
            _groupRepository = groupRepository;
            _cityRepository = cityRepository;
            _topicRepository = topicRepository;
            _userRepository = userRepository;
            _eventStore = eventStore;
        }

        public async Task<Unit> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var isExistsOrganizer = await _userRepository.ExistsUser(request.OrganizerId);

            if (!isExistsOrganizer)
                throw new OrganizerNotFoundException();

            var isExistsCity = await _cityRepository.ExistsCity(request.CityId);

            if (!isExistsCity)
                throw new CityNotFoundException();

            foreach (var topicId in request.TopicIds)
            {
                var isExistsTopic = await _topicRepository.ExistsTopic(topicId);

                if (!isExistsTopic)
                    throw new TopicNotFoundException();
            }

            var groupId = Guid.NewGuid();

            await _groupRepository.CreateGroup(groupId, request);

            await _eventStore.SaveAsync<V1.GroupCreated>(new V1.GroupCreated[] { new V1.GroupCreated(groupId, request.Name, request.OrganizerId) });

            return Unit.Value;
        }
    }
}
