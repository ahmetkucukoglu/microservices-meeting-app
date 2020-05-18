namespace Meeting.GatewayAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/groups")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsApi _groupsApi;
        private readonly IEventsApi _eventsApi;

        public GroupsController(IGroupsApi groupsApi, IEventsApi eventsApi)
        {
            _groupsApi = groupsApi;
            _eventsApi = eventsApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupsApi.GetAll();

            return Ok(groups);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
        {
            var result = await _groupsApi.Create(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tasks = new List<Task>();

            var group = _groupsApi.GetById(id);
            tasks.Add(group);

            Task<ApiSuccessResponse<GetMemberInfoResponse>> memberInfo = default;

            if (User.Identity.IsAuthenticated)
            {
                memberInfo = _groupsApi.MemberInfo(id);
                tasks.Add(memberInfo);
            }

            await Task.WhenAll(tasks);

            var response = new ApiSuccessResponse<GetGroupByIdCompositionResponse>
            {
                Code = 200,
                Result = new GetGroupByIdCompositionResponse
                {
                    Group = group.Result.Result,
                    MemberInfo = User.Identity.IsAuthenticated ? memberInfo.Result.Result : new GetMemberInfoResponse()
                }
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}/active-events")]
        [AllowAnonymous]
        public async Task<IActionResult> ActiveEvents(Guid id)
        {
            var activeEvents = await _eventsApi.GroupEvents(new GetGroupEventsRequest { GroupId = id, Status = "active" });

            var response = new ApiSuccessResponse<IEnumerable<GetGroupEventsResponse>>
            {
                Code = 200,
                Result = activeEvents.Result.OrderByDescending((x) => x.Date)
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}/past-events")]
        [AllowAnonymous]
        public async Task<IActionResult> PastEvents(Guid id)
        {
            var completedEvents = _eventsApi.GroupEvents(new GetGroupEventsRequest { GroupId = id, Status = "completed" });
            var cancelledEvents = _eventsApi.GroupEvents(new GetGroupEventsRequest { GroupId = id, Status = "cancelled" });

            await Task.WhenAll(completedEvents, cancelledEvents);

            var events = new List<GetGroupEventsResponse>();
            events.AddRange(completedEvents.Result.Result);
            events.AddRange(cancelledEvents.Result.Result);

            var response = new ApiSuccessResponse<IEnumerable<GetGroupEventsResponse>>
            {
                Code = 200,
                Result = events.OrderByDescending((x) => x.Date)
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("urlkey/{urlKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUrlKey(string urlKey)
        {
            var group = await _groupsApi.GetByUrlKey(urlKey);

            var memberInfoResponse = new GetMemberInfoResponse();

            if (User.Identity.IsAuthenticated)
            {
                memberInfoResponse = (await _groupsApi.MemberInfo(group.Result.Id)).Result;
            }

            var response = new ApiSuccessResponse<GetGroupByIdCompositionResponse>
            {
                Code = 200,
                Result = new GetGroupByIdCompositionResponse
                {
                    Group = group.Result,
                    MemberInfo = memberInfoResponse
                }
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("find")]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery] FindGroupsRequest request)
        {
            var groups = await _groupsApi.Find(request);

            return Ok(groups);
        }

        [HttpPatch]
        [Route("{id}/join")]
        public async Task<IActionResult> Join(Guid id)
        {
            var result = await _groupsApi.Join(id);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{id}/leave")]
        public async Task<IActionResult> Leave(Guid id)
        {
            var result = await _groupsApi.Leave(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/members")]
        [AllowAnonymous]
        public async Task<IActionResult> Members(Guid id)
        {
            var result = await _groupsApi.Members(id);

            return Ok(result);
        }
    }
}