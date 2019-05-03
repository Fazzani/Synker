using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Synker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseBeatPulse(options =>
            {
                options.ConfigurePath(path: "liveness") //default hc
                     .ConfigureTimeout(milliseconds: 1500) // default -1 infinitely
                     .ConfigureDetailedOutput(detailedOutput: true, includeExceptionMessages: true); //default (true,false)
            }).UseStartup<Startup>();
    }
}
