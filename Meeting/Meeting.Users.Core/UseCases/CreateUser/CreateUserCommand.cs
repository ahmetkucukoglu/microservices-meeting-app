namespace Meeting.Users.Core
{
    using MediatR;

    public class CreateUserCommand : IRequest
    {
        public CreateUserCommand(string userId, string name, string email)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidNameException();
            }

            if (string.IsNullOrEmpty(email)) //TODO email address regex
            {
                throw new InvalidEmailException();
            }

            UserId = userId;
            Name = name;
            Email = email;
        }

        public string UserId { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
