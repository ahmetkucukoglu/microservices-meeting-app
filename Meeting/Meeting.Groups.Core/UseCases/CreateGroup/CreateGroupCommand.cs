namespace Meeting.Groups.Core
{
    using MediatR;
    using System;
    using System.Collections.Generic;

    public class CreateGroupCommand : IRequest
    {
        public CreateGroupCommand(string name, string description, string organizerId, Guid cityId, List<Guid> topicIds)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidNameException();
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidDescriptionException();
            }

            if (string.IsNullOrEmpty(organizerId))
            {
                throw new InvalidOrganizerIdException();
            }

            if (cityId == null)
            {
                throw new InvalidCityIdException();
            }

            if (topicIds == null || topicIds.Count == 0)
            {
                throw new InvalidTopicIdsException();
            }

            Name = name;
            Description = description;
            UrlKey = getUrlKey();
            OrganizerId = organizerId;
            CityId = cityId;
            TopicIds = topicIds;
        }

        public string Name { get; }
        public string Description { get; }
        public string UrlKey { get; }
        public string OrganizerId { get; }
        public Guid CityId { get; }
        public List<Guid> TopicIds { get; }

        internal string getUrlKey()
        {
            var slugHelper = new SlugHelper();

            return slugHelper.GenerateSlug(Name);
        }
    }
}
