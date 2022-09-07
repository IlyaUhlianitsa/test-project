using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using Implicat.Application.Infrastructure;
using Implicat.Configuration.Options;
using Microsoft.Extensions.Options;

namespace Implicat.Configuration.Hangfire
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangfire(this IServiceCollection services)
        {
            services.AddSingleton<HangfireLogJobAttribute>();
            services.AddHangfire(
                c => c
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMemoryStorage(new MemoryStorageOptions { FetchNextJobTimeout = TimeSpan.FromMinutes(10) }));
            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard(
                "/core/background-jobs",
                new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } });

            GlobalJobFilters.Filters.Add(app.ApplicationServices.GetRequiredService<HangfireLogJobAttribute>());

            var recurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
            var options = app.ApplicationServices.GetRequiredService<IOptions<BackgroundJobsOptions>>().Value;

            RegisterJobs(options, recurringJobManager);

            return app;
        }

        private static void RegisterJobs(BackgroundJobsOptions options, IRecurringJobManager recurringJobManager)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes().Where(c => c.GetInterfaces().Contains(typeof(IBackgroundJob))))
                .ToList();

            foreach (var (name, schedules) in options.Schedules)
            {
                foreach (var schedule in schedules)
                {
                    var type = types.First(x => x.Name == $"{name}BackgroundJob");
                    recurringJobManager.AddOrUpdate(
                        $"{type.Name} '{schedule}'",
                        new Job(type, type.GetMethod(nameof(IBackgroundJob.Run)), CancellationToken.None),
                        schedule,
                        TimeZoneInfo.Local);
                }
            }
        }
    }
}