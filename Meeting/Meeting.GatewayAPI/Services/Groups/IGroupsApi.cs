namespace Meeting.GatewayAPI
{
    using Refit;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGroupsApi
    {
        [Post("")]
        Task<ApiSuccessResponse<string>> Create([Body]CreateGroupRequest request);

        [Get("/{id}")]
        Task<ApiSuccessResponse<GetGroupByIdResponse>> GetById(Guid id);

        [Get("/urlkey/{urlkey}")]
        Task<ApiSuccessResponse<GetGroupByIdResponse>> GetByUrlKey(string urlkey);

        [Get("/find")] 
        Task<ApiSuccessResponse<List<FindGroupsResponse>>> Find([Query] FindGroupsRequest request);

        [Patch("/{id}/join")]
        Task<ApiSuccessResponse<string>> Join(Guid id);

        [Patch("/{id}/leave")]
        Task<ApiSuccessResponse<string>> Leave(Guid id);

        [Get("/{id}/members")]
        Task<ApiSuccessResponse<IEnumerable<GetMembersResponse>>> Members(Guid id);

        [Get("/{id}/member-info")]
        Task<ApiSuccessResponse<GetMemberInfoResponse>> MemberInfo(Guid id);

        [Get("")]
        Task<ApiSuccessResponse<IEnumerable<GetGroupsResponse>>> GetAll();
    }
}
