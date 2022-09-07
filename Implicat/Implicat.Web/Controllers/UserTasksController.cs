using AutoMapper;
using Implicat.Application.UserTasks.Queries;
using Implicat.Domain.UserTasks;
using Implicat.Models.UserTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Implicat.Controllers
{
    [Produces("application/json")]
    [Route("user-tasks")]
    public class UserTasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserTasksController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserTaskListItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetList(
            [FromQuery]GetUserTasksRequest requestApi,
            CancellationToken cancellationToken)
        {
            var request = _mapper.Map<GetUserTasksRequest, GetUserTasksQuery>(requestApi);

            var response = await _mediator.Send(request, cancellationToken);

            var tasks = _mapper.Map<List<UserTask>, List<UserTaskListItem>>(response);

            return Ok(tasks);
        }
    }
}