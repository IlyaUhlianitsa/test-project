namespace Implicat.Configuration.Options
{
    public class BackgroundJobsOptions
    {
        public static string SectionName => "BackgroundJobs";

        public Dictionary<string, string[]> Schedules { get; set; } = null!;
    }
}