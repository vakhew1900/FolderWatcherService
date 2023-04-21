﻿using FolderWatcherBackgroundProgram.config;
using FolderWatcherBackgroundProgram.instruments.folderWatcher;
using Microsoft.Extensions.Options;

namespace FolderWatcherBackgroundProgram.instruments
{
    public class FolderWatcherService : BackgroundService
    {

        PathConfig _pathConfig;
        ILogger<FolderWatcherService> _logger;
        public FolderWatcherService(IOptions<PathConfig> options, ILogger<FolderWatcherService> logger)
        {
            _pathConfig = options.Value;
            _logger = logger;
        }




        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string path = _pathConfig.Path;
            Console.WriteLine(path);
            var folderWatcher = new LoggingFolderWatcher(path);

            _logger.LogInformation($"Start watching for ${_pathConfig.Path}");

            while (!stoppingToken.IsCancellationRequested)
            {

            }

            _logger.LogInformation($"finish watching for {_pathConfig.Path}");

            folderWatcher.WriteInfoAboutChangeFolder();
            Task.Delay(2000).Wait();

            return Task.CompletedTask;
        }

       
    }
}