namespace FolderWatcherBackgroundProgram.instruments
{
    public class FolderWatcherService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Test");
            return Task.CompletedTask;
            /* throw new NotImplementedException();*/
        }
    }
}