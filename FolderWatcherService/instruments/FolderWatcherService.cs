using FolderWatcherBackgroundProgram.config;
using FolderWatcherBackgroundProgram.instruments.folderWatcher;
using Microsoft.Extensions.Options;

namespace FolderWatcherBackgroundProgram.instruments
{
    public class FolderWatcherService : BackgroundService
    {

        private IOptionsMonitor<PathConfig> _optionsMonitor;
        private ILogger<FolderWatcherService> _logger;
        protected internal virtual IDisposable _changeListener { get; }
        private CancellationToken _cancellationToken;
        public FolderWatcherService(IOptionsMonitor<PathConfig> optionsMonitor, ILogger<FolderWatcherService> logger)
        {
           _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _changeListener = _optionsMonitor.OnChange(listener: OnMyOptionsChange);
           _logger = logger;
        
             
        }

        private void OnMyOptionsChange(PathConfig arg1, string? arg2)
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            
            try
            {
                _cancellationToken = stoppingToken;
                string path = _optionsMonitor.CurrentValue.Path;

                Console.WriteLine(path);
                var folderWatcher = new LoggingFolderWatcher(path);


                _logger.LogInformation($"Start watching for {path}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    
                }

                _logger.LogInformation($"finish watching for {path}");

                folderWatcher.WriteInfoAboutChangeFolder();
                Task.Delay(200).Wait();
            }
            catch (System.ArgumentException ex)
            {
                _logger.LogError(ex.Message);
            }




            return Task.CompletedTask;
        }

       
        public override void Dispose()
        {
            _changeListener.Dispose();
            base.Dispose();
        }


    }
}