namespace Implicat.Application.Infrastructure
{
    public interface IBackgroundJob
    {
        Task Run(CancellationToken cancellationToken);
    }
}