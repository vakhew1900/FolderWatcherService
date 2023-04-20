using FolderWatcherBackgroundProgram.instruments;

namespace FolderWatcherBackgroundProgram
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<FolderWatcherService>();
                services.AddLogging();
            })
            .ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddConsole();
                configLogging.AddDebug();
            })
            .RunConsoleAsync();
        }
    }
}