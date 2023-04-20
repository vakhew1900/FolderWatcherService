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
            })
            .RunConsoleAsync();
        }
    }
}