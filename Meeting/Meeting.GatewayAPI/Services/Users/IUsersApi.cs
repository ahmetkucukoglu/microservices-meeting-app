namespace Meeting.GatewayAPI
{
    using Refit;
    using System.Threading.Tasks;

    public interface IUsersApi
    {
        [Post("")]
        Task<ApiSuccessResponse<string>> Create([Body]CreateUserRequest request);
    }
}
