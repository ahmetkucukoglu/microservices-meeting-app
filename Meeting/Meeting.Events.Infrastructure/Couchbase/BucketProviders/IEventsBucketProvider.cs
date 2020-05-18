namespace Meeting.Events.Infrastructure
{
    using Couchbase.Extensions.DependencyInjection;

    public interface IEventsBucketProvider : INamedBucketProvider { }
}
