using System;
using System.IO;
using System.Threading;

namespace FileWatch
{
    public class FileWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private int a = 0;
        public FileWatcher(string path,string filter = null)
        {
            _fileSystemWatcher = new FileSystemWatcher(path);
            //_fileSystemWatcher.InternalBufferSize = 8192;

            //_fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
            //                                  | NotifyFilters.CreationTime
            //                                  | NotifyFilters.DirectoryName
            //                                  | NotifyFilters.FileName
            //                                  | NotifyFilters.LastAccess
            //                                  | NotifyFilters.LastWrite
            //                                  | NotifyFilters.Security
            //                                  | NotifyFilters.Size;
            //_fileSystemWatcher.Filter = filter;
            _fileSystemWatcher.Created += OnFileSystemWatcherOnCreated;
            _fileSystemWatcher.Changed += OnFileSystemWatcherOnChanged;
            _fileSystemWatcher.Deleted += OnFileSystemWatcherOnDeleted;
            _fileSystemWatcher.Error += OnFileSystemWatcherOnError;
        }

        private void OnFileSystemWatcherOnError(object sender, ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFileSystemWatcherOnDeleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFileSystemWatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }


        public void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
        }
        public void End()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }
        private void OnFileSystemWatcherOnCreated(object sender, FileSystemEventArgs e)
        {
            FileRead(e.FullPath);
        }

        private void FileRead(string path)
        {
            try
            {
                Thread.Sleep(100);

                using (var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    var bytes = new byte[20480];
                    file.Read(bytes, 0, bytes.Length);
                }
                Console.WriteLine(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

        }


    }
}
