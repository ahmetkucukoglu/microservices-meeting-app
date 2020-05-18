namespace Meeting.Groups.API.Controllers
{
    using MediatR;
    using Meeting.Groups.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("api/groups")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var groups = await _mediator.Send(new GetGroupsQuery(User.Identity.Name), cancellationToken);

            return Ok(groups);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateGroupCommand(request.Name, request.Description, User.Identity.Name, request.CityId, request.TopicIds), cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var group = await _mediator.Send(new GetGroupByIdQuery(id), cancellationToken);

            return Ok(group);
        }

        [HttpGet]
        [Route("urlkey/{urlKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUrlKey(string urlKey, CancellationToken cancellationToken)
        {
            var group = await _mediator.Send(new GetGroupByUrlKeyQuery(urlKey), cancellationToken);

            return Ok(group);
        }

        [HttpGet]
        [Route("find")]
        [AllowAnonymous]
        public async Task<IActionResult> Find([FromQuery] FindGroupsRequest request, CancellationToken cancellationToken)
        {
            var groups = await _mediator.Send(new FindGroupsQuery(request.CityId, request.TopicIds), cancellationToken);

            return Ok(groups);
        }

        [HttpPatch]
        [Route("{id}/join")]
        public async Task<IActionResult> Join(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new JoinGroupCommand(id, User.Identity.Name), cancellationToken);

            return Ok();
        }

        [HttpPatch]
        [Route("{id}/leave")]
        public async Task<IActionResult> Leave(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new LeaveGroupCommand(id, User.Identity.Name), cancellationToken);

            return Ok();
        }

        [HttpGet]
        [Route("{id}/members")]
        [AllowAnonymous]
        public async Task<IActionResult> Members(Guid id, CancellationToken cancellationToken)
        {
            var members = await _mediator.Send(new GetMembersQuery(id), cancellationToken);

            return Ok(members);
        }

        [HttpGet]
        [Route("{id}/member-info")]
        public async Task<IActionResult> MemberInfo(Guid id, CancellationToken cancellationToken)
        {
            var memberInfo = await _mediator.Send(new GetMemberInfoQuery(id, User.Identity.Name), cancellationToken);

            return Ok(memberInfo);
        }
    }
}