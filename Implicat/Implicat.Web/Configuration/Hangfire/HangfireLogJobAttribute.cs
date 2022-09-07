using Hangfire.Server;
using Implicat.Application.Infrastructure;

namespace Implicat.Configuration.Hangfire
{
    public class HangfireLogJobAttribute : IServerFilter
    {
        private readonly IClock _clock;
        private readonly ILogger<HangfireLogJobAttribute> _logger;

        public HangfireLogJobAttribute(
            ILogger<HangfireLogJobAttribute> logger,
            IClock clock)
        {
            _logger = logger;
            _clock = clock;
        }

        public void OnPerforming(PerformingContext filterContext)
        {
            _logger.LogInformation(
                "Background job {JobName} '{JobId}' has started",
                filterContext.BackgroundJob.Job.Type.Name,
                filterContext.BackgroundJob.Id);
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            var proceededMs = (_clock.Now.ToUniversalTime() - filterContext.BackgroundJob.CreatedAt).TotalMilliseconds;

            var exception = filterContext.Exception;
            if (exception != null)
            {
                _logger.LogError(
                    exception,
                    "Background job {JobName} '{JobId}' has finished with error for {ProceededMs} ms",
                    filterContext.BackgroundJob.Job.Type.Name,
                    filterContext.BackgroundJob.Id,
                    proceededMs);
            }
            else
            {
                _logger.LogInformation(
                    "Background job {JobName} '{JobId}' has finished successfully for {ProceededMs} ms",
                    filterContext.BackgroundJob.Job.Type.Name,
                    filterContext.BackgroundJob.Id,
                    proceededMs);
            }
        }
    }
}