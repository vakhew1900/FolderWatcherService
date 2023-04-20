namespace FolderWatcherBackgroundProgram.instruments
{
    public class FolderWatcherService : BackgroundService
    {
       
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string path = @"E:\3_course\2sem\test";
            var folderWatcher = new FolderWatcher(path);

            while (!stoppingToken.IsCancellationRequested)
            {

            }

            folderWatcher.WriteInfoAboutChangeFolder();
            Task.Delay(5000).Wait();

            return Task.CompletedTask;
        }
    }
}