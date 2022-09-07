using AutoMapper;
using Implicat.Application.UserTasks.Queries;
using Implicat.Domain.UserTasks;
using Implicat.Models.UserTasks;

namespace Implicat.Mapping
{
    public class UserTasksMappingProfile : Profile
    {
        public UserTasksMappingProfile()
        {
            CreateMap<GetUserTasksRequest, GetUserTasksQuery>();
            CreateMap<UserTask, UserTaskListItem>();
        }
    }
}