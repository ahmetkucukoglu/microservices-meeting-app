namespace Meeting.Users.Core
{
    using System;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task CreateUser(string userId, CreateUserCommand createUserCommand);
    }
}
