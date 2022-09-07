using Implicat.Domain.UserTasks;

namespace Implicat.Models.UserTasks
{
    public class GetUserTasksRequest
    {
        public Guid UserId { get; set; }

        public UserTaskStatus[]? Statuses { get; set; }

        public DateTime? DueDateFrom { get; set; }

        public DateTime? DueDateTo { get; set; }
    }
}