namespace Meeting.GatewayAPI
{
    using Refit;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICitiesApi
    {
        [Post("")]
        Task<ApiSuccessResponse<string>> Create([Body]CreateCityRequest request);

        [Get("/find")] 
        Task<ApiSuccessResponse<List<GetCitiesByTermResponse>>> Find([Query] GetCitiesByTermRequest request);
    }
}
