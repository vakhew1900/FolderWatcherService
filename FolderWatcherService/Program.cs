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
            var builder = new HostBuilder();

            builder.ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddConsole();
                configLogging.AddDebug();
            });

             builder.ConfigureHostConfiguration(hostConfig =>
                {
                    hostConfig.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json", optional: false, reloadOnChange: true).AddEnvironmentVariables().Build();
                });



             builder.ConfigureServices((hostContext, services) =>
            {
               
                services.AddHostedService<FolderWatcherService>();
                services.AddLogging();
                var pathConfig = hostContext.Configuration.GetSection("PathConfig");
                services.Configure<PathConfig>(pathConfig);
            });

            try
            {
                await builder.RunConsoleAsync();
            }
            catch (System.IO.InvalidDataException)
            {
                Console.WriteLine("Incorrect config file. Please path for config and  configContent");
            }
        }
    }
}