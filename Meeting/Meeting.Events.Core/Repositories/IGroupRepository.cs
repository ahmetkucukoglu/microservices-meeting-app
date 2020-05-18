namespace Meeting.Events.Core
{
    using System;
    using System.Threading.Tasks;

    public interface IGroupRepository
    {
        Task<bool> ExistsGroup(Guid groupId);
        Task<bool> ExistsGroup(Guid groupId, string organizerId);
    }
}
