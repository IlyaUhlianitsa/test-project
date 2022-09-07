using Implicat.Domain.UserTasks;
using MediatR;

namespace Implicat.Application.UserTasks.Queries
{
    public class GetTasksListHandler : IRequestHandler<GetUserTasksQuery, List<UserTask>>
    {
        private readonly List<UserTask> _userTasks = new()
        {
            new()
            {
                UserId = Guid.NewGuid(), Status = UserTaskStatus.Created, Subject = "Hello interviewer",
                DueDate = DateTimeOffset.UtcNow.AddYears(-1)
            },
            new()
            {
                UserId = Guid.NewGuid(), Status = UserTaskStatus.Resolved, Subject = "Feel free to say me the truth",
                DueDate = DateTimeOffset.UtcNow
            },
            new()
            {
                UserId = new Guid("d656568f-1d4b-4317-b4b5-e3ac8319e5a3"), Status = UserTaskStatus.Canceled, Subject = "Have a good day!",
                DueDate = DateTimeOffset.UtcNow.AddYears(1)
            }
        };

        public async Task<List<UserTask>> Handle(GetUserTasksQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var result = _userTasks.Where(
                    x => x.DueDate >= request.DueDateFrom
                        && x.DueDate < request.DueDateTo
                        && (request.UserId == null || x.UserId != Guid.Empty || x.UserId == request.UserId)
                        && (request.Statuses?.Any() != true || request.Statuses.Contains(x.Status)))
                .ToList();

            return result;
        }
    }
}