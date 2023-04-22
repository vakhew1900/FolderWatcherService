using Microsoft.Extensions.Options;

namespace FolderWatcherBackgroundProgram.instruments.folderWatcher
{

    public class LoggingFolderWatcher : FolderWatcher
    {
          
        private  ILogger<LoggingFolderWatcher> _logger;

        public LoggingFolderWatcher(string path) : base(path)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(option =>
                {
                    option.IncludeScopes = false;
                    option.TimestampFormat = "[HH:mm:ss]";
                });

                builder.AddDebug();
            });

           
            _logger = loggerFactory.CreateLogger<LoggingFolderWatcher>();  


        }


        protected override void OnChanged(object sender, FileSystemEventArgs e)
        {
            base.OnChanged(sender, e);
            _logger.LogInformation($"Changed : {e.FullPath}");
        
        }

        protected override void OnCreated(object sender, FileSystemEventArgs e)
        {
            base.OnCreated(sender, e);
            _logger.LogInformation($"Created : {e.FullPath}");
        }

        protected override void OnDeleted(object sender, FileSystemEventArgs e)
        {
            base.OnDeleted(sender, e);
            _logger.LogInformation($"Deleted : {e.FullPath}");
        }

        protected override void OnRenamed(object sender, RenamedEventArgs e)
        {
            base.OnRenamed(sender, e);
            _logger.LogInformation($"Renamed : {e.FullPath}");
        }

        protected override void OnError(object sender, ErrorEventArgs e)
        {
            if (e.GetException() != null) {
                _logger.LogError($"Message: {e.GetException().Message}");
            }
        }
    }
}
