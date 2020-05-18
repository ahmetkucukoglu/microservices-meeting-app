namespace Meeting.Groups.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGroupRepository
    {
        Task<bool> ExistsGroup(Guid groupId);
        Task CreateGroup(Guid groupId, CreateGroupCommand createGroupCommand);
        Task<List<FindGroupsQueryResult>> FindGroups(FindGroupsQuery findGroupsQuery);
        Task JoinGroup(JoinGroupCommand joinGroupCommand);
        Task LeaveGroup(LeaveGroupCommand leaveGroupCommand);
        Task<GetGroupByIdQueryResult> GetGroupById(GetGroupByIdQuery getGroupByIdQuery);
        Task<GetGroupByIdQueryResult> GetGroupByUrlKey(GetGroupByUrlKeyQuery getGroupByUrlKeyQuery);
        Task<IEnumerable<GetMembersQueryResult>> GetMembers(GetMembersQuery getMembersQuery);
        Task<GetMemberInfoQueryResult> GetMemberInfo(GetMemberInfoQuery getMemberInfoQuery);
        Task<IEnumerable<GetGroupsQueryResult>> GetGroups(GetGroupsQuery getGroupsQuery);
    }
}
