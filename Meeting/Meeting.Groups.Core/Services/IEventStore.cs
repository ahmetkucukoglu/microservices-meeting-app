namespace Meeting.Groups.Core
{
    using System.Threading.Tasks;

    public interface IEventStore
    {
        Task SaveAsync<T>(T[] events) where T : Event;
    }
}
