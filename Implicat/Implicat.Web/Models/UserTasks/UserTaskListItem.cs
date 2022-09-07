using Implicat.Domain.UserTasks;

namespace Implicat.Models.UserTasks;

public class UserTaskListItem
{
    public Guid UserId { get; set; }

    public string Subject { get; set; } = null!;

    public UserTaskStatus Status { get; set; }

    public DateTimeOffset DueDate { get; set; }
}