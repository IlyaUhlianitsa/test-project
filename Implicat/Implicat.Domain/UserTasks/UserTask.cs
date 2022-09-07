namespace Implicat.Domain.UserTasks;

public class UserTask
{
    public Guid UserId { get; set; }

    public string Subject { get; set; } = null!;

    public UserTaskStatus Status { get; set; }

    public DateTimeOffset DueDate { get; set; }
}