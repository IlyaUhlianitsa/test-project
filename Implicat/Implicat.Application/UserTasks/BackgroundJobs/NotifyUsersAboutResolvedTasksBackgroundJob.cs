using Implicat.Application.Infrastructure;

namespace Implicat.Application.UserTasks.BackgroundJobs;

public class NotifyUsersAboutResolvedTasksBackgroundJob : IBackgroundJob
{
    public Task Run(CancellationToken cancellationToken)
    {
        /// TODO: send emails to users about Resolved tasks

        return Task.CompletedTask;
    }
}