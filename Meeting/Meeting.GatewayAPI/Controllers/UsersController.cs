namespace Meeting.GatewayAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApi _usersApi;

        public UsersController(IUsersApi usersApi)
        {
            _usersApi = usersApi;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var result = await _usersApi.Create(request);

            return Ok(result);
        }
    }
}