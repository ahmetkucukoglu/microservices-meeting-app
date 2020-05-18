namespace Meeting.GatewayAPI
{
    using Refit;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITopicsApi
    {
        [Post("")]
        Task<ApiSuccessResponse<string>> Create([Body]CreateTopicRequest request);

        [Get("/find")] 
        Task<ApiSuccessResponse<List<GetTopicsByTermResponse>>> Find([Query] GetTopicsByTermRequest request);
    }
}
