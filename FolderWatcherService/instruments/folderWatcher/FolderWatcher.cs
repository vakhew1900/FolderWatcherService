using System.Collections;
using System.Collections.Generic;

namespace FolderWatcherBackgroundProgram.instruments.folderWatcher
{

    public class FolderWatcher
    {

        private FileSystemWatcher _watcher;

        private List<string> _createdList = new();
        private List<string> _changedList = new();
        private List<string> _deletedList = new();
        private List<string> _renamedList = new();

        public FolderWatcher(string path)
        {

            if (Directory.Exists(path) == false)
            {
                throw new System.ArgumentException("File or Directory is not Exist");
            }

            _watcher = new FileSystemWatcher(@path)
            {
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size
            };

            _watcher.Changed += OnChanged;
            _watcher.Created += OnCreated;
            _watcher.Deleted += OnDeleted;
            _watcher.Renamed += OnRenamed;
            _watcher.Error += OnError;

            _watcher.Filter = "*.*";
            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;

        }

        protected virtual void OnChanged(object sender, FileSystemEventArgs e)
        {
            
            _changedList.Add(e.Name);
        }

        protected virtual void OnCreated(object sender, FileSystemEventArgs e)
        {
            _createdList.Add(e.Name);
        }

        protected virtual void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _deletedList.Add(e.Name);
        }

        protected virtual void OnRenamed(object sender, RenamedEventArgs e)
        {
            _renamedList.Add($"{e.OldName} -> {e.Name}");
        }

        protected virtual void OnError(object sender, ErrorEventArgs e)
        {
            PrintException(e.GetException());
        }

        private void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }

        private void WriteList(string title, List<string> list)
        {
            Console.WriteLine(title + ":");

            list.ForEach(elem => Console.WriteLine("-" + elem));
        }


        public void WriteInfoAboutChangeFolder()
        {
            WriteList("Созданы", _createdList);
            WriteList("Переименованы", _renamedList);
            WriteList("Обновлены", _changedList);
            WriteList("Удалены", _deletedList);
        }

    }
}
