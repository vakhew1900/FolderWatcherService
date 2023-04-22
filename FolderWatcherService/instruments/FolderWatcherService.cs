using FolderWatcherBackgroundProgram.config;
using FolderWatcherBackgroundProgram.instruments.folderWatcher;
using Microsoft.Extensions.Options;
using System.IO;

namespace FolderWatcherBackgroundProgram.instruments
{
    public class FolderWatcherService : BackgroundService
    {

        private IOptionsMonitor<PathConfig> _optionsMonitor;
        private ILogger<FolderWatcherService> _logger;
        protected internal virtual IDisposable _changeListener { get; }
        private FolderWatcher _folderWatcher;
        
        private bool isError = false;
        private string errorMessage;

        public FolderWatcherService(IOptionsMonitor<PathConfig> optionsMonitor, ILogger<FolderWatcherService> logger)
        {
           _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _changeListener = _optionsMonitor.OnChange(listener: OnMyOptionsChange);
           _logger = logger;
        
             
        }

        private void OnMyOptionsChange(PathConfig arg1, string? arg2)
        {
            if (_folderWatcher != null)
            {
                finishMessage();
            }

            _logger.LogInformation($"Update watching folder. Now it`s {_optionsMonitor.CurrentValue.Path}");

            
            _folderWatcher = null; //специально обнуляем чтобы не следить не за одной из папок

            try
            {

                string path = _optionsMonitor.CurrentValue.Path;
               _folderWatcher = new LoggingFolderWatcher(path);
            }
            catch(System.ArgumentException ex)
            {
                isError = true;
                Console.WriteLine(isError);
                errorMessage = ex.Message;
            }
            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            
            try
            {
               
                string path = _optionsMonitor.CurrentValue.Path;
                _folderWatcher = new LoggingFolderWatcher(path);


                _logger.LogInformation($"Start watching for {path}");

                while (!stoppingToken.IsCancellationRequested)
                {
                    
                    if (isError == true) 
                    {
                        isError = false;
                        _logger.LogError(errorMessage);
                    }
                }

                finishMessage();


                Task.Delay(200).Wait();
            }
            catch (System.ArgumentException ex)
            {
                _logger.LogError(ex.Message);
            }




            return Task.CompletedTask;
        }


        private void finishMessage()
        {
            _logger.LogInformation($"finish watching for {_folderWatcher.Path}");
            _folderWatcher.WriteInfoAboutChangeFolder();
        }
       
        public override void Dispose()
        {
            _changeListener.Dispose();
            base.Dispose();
        }


    }
}