using FolderWatcherBackgroundProgram.instruments.folderWatcher;

namespace FolderWatcherBackgroundProgram.instruments
{
    public class FolderWatcherService : BackgroundService
    {
       
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string path = @"E:\3_course\2sem\test";
            var folderWatcher = new LoggingFolderWatcher(path);

            while (!stoppingToken.IsCancellationRequested)
            {

            }

            folderWatcher.WriteInfoAboutChangeFolder();
            Task.Delay(2000).Wait();

            return Task.CompletedTask;
        }
    }
}