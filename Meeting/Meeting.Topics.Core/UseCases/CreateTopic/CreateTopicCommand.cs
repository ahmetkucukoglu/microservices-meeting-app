namespace Meeting.Topics.Core
{
    using MediatR;

    public class CreateTopicCommand : IRequest
    {
        public CreateTopicCommand(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidNameException();
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidDescriptionException();
            }

            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
