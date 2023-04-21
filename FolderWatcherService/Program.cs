using FolderWatcherBackgroundProgram.config;
using FolderWatcherBackgroundProgram.instruments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FolderWatcherBackgroundProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder()
            .ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddConsole();
                configLogging.AddDebug();
            })
            .ConfigureHostConfiguration(hostConfig =>
               {
                   hostConfig.AddJsonFile("config.json");
               })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<FolderWatcherService>();
                services.AddLogging();
                var pathConfig = hostContext.Configuration.GetSection("PathConfig");
                services.Configure<PathConfig>(pathConfig);
            })
            .RunConsoleAsync();
        }
    }
}