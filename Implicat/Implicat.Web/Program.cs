using Microsoft.AspNetCore;

namespace Implicat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();

            await webHost.RunAsync();
        }
    }
}