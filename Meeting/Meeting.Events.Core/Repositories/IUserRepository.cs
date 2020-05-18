namespace Meeting.Events.Core
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<bool> ExistsUser(string userId);
    }
}
