namespace FolderWatcherBackgroundProgram.instruments.folderWatcher
{
    public class ConsoleFolderWatcher : FolderWatcher
    {
        public ConsoleFolderWatcher(string path) : base(path)
        {
            
        }


        protected override void OnChanged(object sender, FileSystemEventArgs e)
        {
            base.OnChanged(sender, e);
            Console.WriteLine($"Changed {e.FullPath}");
        }

        protected override void OnCreated(object sender, FileSystemEventArgs e)
        {
            base.OnCreated(sender, e);
            Console.WriteLine($"Created {e.FullPath}");
        }

        protected override void OnDeleted(object sender, FileSystemEventArgs e)
        {
            base.OnDeleted(sender, e);
            Console.WriteLine($"Deleted {e.FullPath}");
        }

        protected override void OnRenamed(object sender, RenamedEventArgs e)
        {
            base.OnRenamed(sender, e);
            Console.WriteLine($"Renamed {e.OldFullPath} -> {e.FullPath} ");
        }

        protected override void OnError(object sender, ErrorEventArgs e)
        {
            base.OnError(sender, e);
        }

    }
}
