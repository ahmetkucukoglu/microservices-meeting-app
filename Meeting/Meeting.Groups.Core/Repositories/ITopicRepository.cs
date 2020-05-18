namespace Meeting.Groups.Core
{
    using System;
    using System.Threading.Tasks;

    public interface ITopicRepository
    {
        Task<bool> ExistsTopic(Guid topicId);
    }
}
