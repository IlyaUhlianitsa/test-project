using Implicat.Domain.UserTasks;
using MediatR;

namespace Implicat.Application.UserTasks.Queries;

public record GetUserTasksQuery(
    Guid? UserId,
    UserTaskStatus[]? Statuses,
    DateTimeOffset? DueDateFrom,
    DateTimeOffset? DueDateTo) : IRequest<List<UserTask>> {}