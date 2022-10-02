using System.IO.Compression;
using Shirehorse.Core.Extensions;

namespace Shirehorse.Core.IO
{
    public abstract class FileOperation
    {
        public class FileOperationEventArgs : EventArgs
        {
            public FileOperationEventArgs(FileOperation operation)
            {
                Operation = operation;
            }
            public FileOperation Operation { get; set; }
            public Exception? Exception { get; set; }

        }
        public const long LargeFileSize = 1048576; // bytes, 1024^2
        public const string FileExistsExceptionString = "Destination file path already exists";

        public abstract string Description { get; }
        public virtual string Status { get; protected set; } = "";
        public virtual double Progress { get; set; } = 0;
        public virtual long TotalBytes { get; protected set; }
        public virtual long CompletedBytes => (long)(Progress * TotalBytes);
        public bool OverwriteAll { get; set; }
        public bool Completed { get; private set; }
        public bool Error { get; private set; }
        public Exception? CachedException { get; protected set; }
        protected string? ExceptionSource { get; set; }

        internal void Start()
        {
            Task.Run(() =>
            {
                try { Operation(); }
                catch (Exception ex) 
                {
                    Error = true;
                    // cache and handle on the main thread
                    CachedException = ex;

                    // We can use this parameter at the UI
                    if (ExceptionSource is not null) CachedException.Source = ExceptionSource;
                } 
            }
            ).GetAwaiter().OnCompleted(() =>
            {
                if (CachedException is null)
                {
                    Completed = true;
                    Progress = 1;
                    OperationCompleted?.Invoke(this, new FileOperationEventArgs(this));
                }
                else
                {
                    OperationError?.Invoke(this, new FileOperationEventArgs(this) { Exception = CachedException });
                }          
            });
        }

        internal void Reset()
        {
            Error = false;
            CachedException = null;
        }

        internal void Abort()
        {

        }

        protected abstract void Operation();

        public event EventHandler<FileOperationEventArgs>? OperationCompleted;
        public event EventHandler<FileOperationEventArgs>? OperationError;
    }

    public class FileCopyOperation : FileOperation
    {
        public override long TotalBytes => _source.Length;
        public override string Status => $"Copying from {_source.FullName}";

        public override string Description => "Copying file";

        protected readonly FileInfo _source;
        protected readonly FileInfo _destination;

        public FileCopyOperation(string source, string destination)
        {
            try
            {
                _source = new FileInfo(source);
                _destination = new FileInfo(destination);
            }
            catch (Exception ex)
            {
                ex.Source = _source.FullName;
                throw;
            }
            
        }

        protected override void Operation()
        {
            Directory.CreateDirectory(_destination.DirectoryName);

            if (_destination.Exists) 
            {
                if (OverwriteAll) _destination.Delete();
                else
                {
                    ExceptionSource = _destination.FullName;
                    throw new IOException(FileExistsExceptionString);
                } 
            } 

            if (_source.Length > LargeFileSize)
            {
                _source.CopyTo(_destination, delegate (int x)
                {
                    Progress = (double)x / FileInfoExtension.CopyToUpdateCounts;
                });

                _destination.LastWriteTime = _source.LastWriteTime;
            }
            else
                _source.CopyTo(_destination.FullName);
        }
    }

    public class DeleteDirectoryOperation : FileOperation
    {
        public override string Description => "Deleting directory";
        public override string Status { get; protected set; } = "";
        private readonly DirectoryInfo _directory;
        private FileInfo[]? _files;
        private int _itemsDeleted;

        public DeleteDirectoryOperation(string path)
        {
            _directory = new DirectoryInfo(path);
        }

        protected override void Operation()
        {
            _files = _directory.GetFiles("*", SearchOption.AllDirectories);
            int directoryCount = _directory.GetDirectories("*", SearchOption.AllDirectories).Length;
            TotalBytes = _files.Length + directoryCount;
            _itemsDeleted = 0;

            FileInfo? fi = null; // must assign value before enumeration

            Status = $"Deleting {_files.Length} files and {directoryCount} directories in {_directory.FullName}";

            try
            {
                foreach (FileInfo file in _files)
                {
                    fi = file;
                    file.Delete();
                    Progress = (double)_itemsDeleted++ / TotalBytes;
                }
            }
            catch (Exception ex)
            {
                ex.Source = fi?.FullName;
                throw;
            }

            DirectoryInfo? di = null; // must assign value before enumeration

            try
            {
                void RecursiveDelete(DirectoryInfo directory)
                {
                    foreach (var child in directory.GetDirectories("*", SearchOption.TopDirectoryOnly))
                        RecursiveDelete(child);

                    di = directory;
                    directory.Delete();
                    Progress = (double)_itemsDeleted++ / TotalBytes;
                }

                RecursiveDelete(_directory);
            }
            catch (Exception ex)
            {
                ex.Source = di?.FullName;
                throw;
            }
        }
    }

    public class DirectoryMoveOperation : FileOperation
    {
        public override string Description => "Moving directory";
        public override string Status { get; protected set; } = "";
        private readonly DirectoryInfo _source;
        private readonly DirectoryInfo _destination;

        public DirectoryMoveOperation(string source, string destination)
        {
            _source = new DirectoryInfo(source);
            _destination = new DirectoryInfo(destination);
        }
        protected override void Operation()
        {
            Status = $"Moving {_source.FullName} to {_destination.FullName}";

            ExceptionSource = _source.FullName;
            if (_destination.Exists)
            {
                if (OverwriteAll) 
                    _destination.Delete();
                else
                {
                    ExceptionSource = _destination.FullName;
                    throw new IOException(FileExistsExceptionString);
                } 
                    
            }
            
            _source.MoveTo(_destination.FullName);
        }
    }


    public class DirectoryCreateOperation : FileOperation
    {
        public override string Description => "Creating directory";
        public override string Status { get; protected set; } = $"";
        private readonly DirectoryInfo _path;
        public DirectoryCreateOperation(string path) => _path = new DirectoryInfo(path);
        protected override void Operation()
        {
            Status = $"Creating directory {_path.FullName}";
            _path.Create();
        }
    }

    public class UnzipFileOperation : FileOperation
    {
        public override string Description => "Unzipping file";
        public string[] ExcludedExtensions { get; set; } = new string[0];

        public override long TotalBytes => _source.Length;
        private long _totalUncompressedBytes;
        private long _completedBytes = 0;

        private readonly FileInfo _source;
        private readonly DirectoryInfo _destinationDir;
        private Queue<string>? _files;

        public UnzipFileOperation(string zipFile, string destinationDirectory) 
        {
            _source = new FileInfo(zipFile);
            _destinationDir = new DirectoryInfo(destinationDirectory);
            ExceptionSource = _source.FullName;
        }

        private Queue<string> GetFileQueue()
        {
            Queue<string> result = new();

            using var zip = ZipFile.Open(_source.FullName, ZipArchiveMode.Read);
            var entries = zip.Entries
                .Where(x => x.Name != "") // exclude directories
                .Where(x => !ExcludedExtensions.Any(ext => x.Name.Contains('.' + ext))); // exclude extensions

            result = new();
            _totalUncompressedBytes = 1; // add 1 just in case size is 0 as there is division later

            foreach (var entry in entries)
            {
                result.Enqueue(entry.FullName); // store as strings so we can use after zipFile is disposed
                _totalUncompressedBytes += entry.Length;
            }

            return result;
        }

        protected override void Operation()
        {
            _files ??= GetFileQueue();

            using var zip = ZipFile.Open(_source.FullName, ZipArchiveMode.Read);
            while (_files.Count > 0)
            {
                var fileName = _files.Dequeue();

                var file = zip.Entries.Where(x => x.FullName == fileName).First();

                string targetPath = Path.Combine(_destinationDir.FullName, file.FullName);

                if (File.Exists(targetPath))
                {
                    if (OverwriteAll) File.Delete(targetPath);
                    else
                    {
                        ExceptionSource = targetPath;
                        throw new IOException(FileExistsExceptionString);
                    }
                }

                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                Status = $"Extracting {fileName} to {targetPath}" ;

                file.ExtractToFile(targetPath);

                _completedBytes += file.Length;
                Progress = (double)_completedBytes / _totalUncompressedBytes;
            }
        }
    }

}
