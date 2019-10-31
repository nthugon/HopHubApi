using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace HopHubApi
{
    /// <summary>
    /// Program class of application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method of program.
        /// </summary>
        /// <param name="args">Main method arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates and instance of IWebHostBuilder.
        /// </summary>
        /// <param name="args">Arguments passed down from Main.</param>
        /// <returns>An instance of IWebHostBuilder.</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
