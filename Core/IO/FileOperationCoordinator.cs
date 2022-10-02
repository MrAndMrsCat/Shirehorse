using System.Diagnostics;
using Shirehorse.Core.FiniteStateMachines;
using static Shirehorse.Core.IO.FileOperation;
using Shirehorse.Core.Diagnostics.Logging;

namespace Shirehorse.Core.IO
{
    public class FileOperationCoordinator
    {
        public FileOperationCoordinator()
        {
            Logger = new("File IO Coordinator");

            _stateMachine.Logger = Logger;

            BuildFSMTransitions();
            BuildFSM();

            _stateMachine.StateChanged += (s, e) =>
            {
                CoordinatorStateChanged?.Invoke(this, new CoordinatorEventArgs()
                {
                    State = _stateMachine.CurrentState
                });
            };

            _updateMetricsTimer.Elapsed += (s, e) => UpdateMetrics();
        }

        public class FileOperationErrorHandler<TRecoveryOptions> : EventArgs where TRecoveryOptions : Enum
        {
            internal FileOperationErrorHandler(FileOperation operation, string errorMessage)
            {
                Operation = operation;
                ErrorMessage = errorMessage;
            }

            public string ErrorMessage { get; private set; }
            public Exception? Exception => Operation?.CachedException;
            internal FileOperation? Operation { get; private set; }

            public event EventHandler<TRecoveryOptions>? RecoveryOptionSelected;
            public void OnRecoveryOptionSelected(object sender, TRecoveryOptions selectedOption)
            {
                RecoveryOptionSelected?.Invoke(sender, selectedOption);
                Operation = null;
            }
        }

        public class CoordinatorEventArgs : EventArgs 
        { 
            public FileCoor State; 
        }

        public enum FileCoor
        {
            Reset,
            Initializing,
            Running,
            Completed,
            Error,
            Aborted,
        }

        public enum OperationFailedRecoveryOption
        {
            Abort,
            Retry,
        }

        public enum FileExistsRecoveryOption
        {
            Abort,
            Overwrite,
            OverwriteAll
        }

        public string OperationTypeDescription { get; private set; } = "";
        public string? Status => _currentOperation?.Status;
        public double Progress { get; private set; } = 0;
        public long TotalBytes { get; private set; }
        public double MegaBytesPerSecond { get; private set; }
        public int RemainingItems => _operations.Where(op => !op.Completed).Count();
        public bool AllCompleted => RemainingItems == 0;
        public TimeSpan EstimatedCompleteTimeSpan { get; private set; } = new TimeSpan(0);
        public long EstimatedCompleteTimeMilliseconds { get; private set; }
        public bool OverwriteAll { get; set; }
        public FileCoor CurrentState => _stateMachine.CurrentState;
        public bool ShowFormOnStart { get; set; } = true;
        public LogSource Logger { get; set; }

        public event EventHandler<CoordinatorEventArgs>? CoordinatorStateChanged;

        private readonly FiniteStateMachine<FileCoor> _stateMachine = new ();
        private readonly List<FileOperation> _operations = new ();
        private readonly Stopwatch _elapsedTime = new ();
        private readonly System.Timers.Timer _updateMetricsTimer = new () { Interval = 100 };
        


        private void BuildFSMTransitions()
        {
            _stateMachine[FileCoor.Reset, FileCoor.Initializing] = new();
            _stateMachine[FileCoor.Completed, FileCoor.Initializing] = new();

            _stateMachine[FileCoor.Initializing, FileCoor.Running] = new();

            _stateMachine[FileCoor.Running, FileCoor.Completed] = new();
            _stateMachine[FileCoor.Running, FileCoor.Error] = new();
            _stateMachine[FileCoor.Running, FileCoor.Aborted] = new();

            _stateMachine[FileCoor.Error, FileCoor.Running] = new();
            _stateMachine[FileCoor.Error, FileCoor.Aborted] = new();
        }

        private void BuildFSM()
        {
            // Reset
            _stateMachine.ResetAction = () =>
            {
                Clear();
            };

            // Initializing
            _stateMachine[FileCoor.Initializing].EntryAction = () =>
            {
                TotalBytes = _operations.Select(op => op.TotalBytes).Sum() + 1; // prevent divide by 0
                _elapsedTime.Restart();

                ChangeState(FileCoor.Running);
            };

            // Running
            _stateMachine[FileCoor.Running].EntryAction = () =>
            {
                StartMetrics();
                StartNextOperation();
            };

            // Completed
            _stateMachine[FileCoor.Completed].EntryAction = () =>
            {
                Clear();
                PauseMetrics();
                Progress = 1;
            };

            // Error
            _stateMachine[FileCoor.Error].EntryActionWithParameter = (param) =>
            {
                PauseMetrics();

                if (param is FileOperation operation)
                {
                    
                    HandleFileOperationException(operation);
                }
                else throw new ArgumentException("Argument must be type of FileOperation");
            };

            // Aborted
            _stateMachine[FileCoor.Aborted].EntryAction = () =>
            {
                foreach (var op in _operations) op.Abort();
                PauseMetrics();
            };

        }

        public void Start() => ChangeState(FileCoor.Initializing);
        public void Abort() => ChangeState(FileCoor.Aborted);
        public void Reset() => _stateMachine.Reset();
        private void ChangeState(FileCoor state) => _stateMachine.ChangeState(state);


        private FileOperation? _currentOperation;
        private void StartNextOperation()
        {
            UpdateMetrics();

            _currentOperation = null;

            if (_operations.Where(x => !x.Completed).FirstOrDefault() is FileOperation operation)
            {
                Logger?.Log(ILogging.Category.Information, $"Starting {operation.Description}");

                _currentOperation = operation;

                if (OverwriteAll) operation.OverwriteAll = true;

                OperationTypeDescription = operation.Description;
                operation.Start();
            }
            else ChangeState(FileCoor.Completed);
        }



        private void Operation_OperationCompleted(object? sender, FileOperationEventArgs e)
        {
            Logger?.Log(ILogging.Category.Information, $"Completed {e.Operation.Description}");

            StartNextOperation();
        }

        private void Operation_OperationError(object? sender, FileOperationEventArgs e)
        {
            Logger?.Log(ILogging.Category.Error, $"Error during {e.Operation.Description}");

            _stateMachine.ChangeState(FileCoor.Error, e.Operation);
        }

        private void HandleFileOperationException(FileOperation operation)
        {
            switch (operation.CachedException)
            {
                default:

                    string exTypeString = operation.CachedException is null 
                        ? "Cached exception is null, this shouldn't happen!" 
                        : operation.CachedException.GetType().ToString();

                    OnOperationFailed(operation, exTypeString);

                    break;

                case IOException ioex when ioex.Message == FileExistsExceptionString:

                    OnFileExistsException(operation, $"{ioex.Source} already exists, overwrite?");

                    break;

                case IOException ioex when ioex.Message.Contains("it is being used by another process."):

                    OnOperationFailed(operation, $"{ioex.Source} is locked by another process, check for process locks?");

                    break;

                case Exception accessEx when accessEx.Message.Contains("Access to the path") && accessEx.Message.Contains("is denied"):

                    OnOperationFailed(operation, $"{accessEx.Source} access is denied, check for process locks?");

                    break;
            }
        }

        public event EventHandler<FileOperationErrorHandler<OperationFailedRecoveryOption>>? OperationFailedException;
        public void OnOperationFailed(FileOperation operation, string errorMessage)
        {
            if (OperationFailedException is null) // no subscribers to handle error
            {
                _stateMachine.ChangeState(FileCoor.Aborted);
            }
            else
            {
                FileOperationErrorHandler<OperationFailedRecoveryOption> handler = new (operation, errorMessage);

                handler.RecoveryOptionSelected += (s, e) =>
                {
                    switch (e)
                    {
                        default: // Abort

                            ChangeState(FileCoor.Aborted);

                            break;

                        case OperationFailedRecoveryOption.Retry:

                            operation.Reset();

                            ChangeState(FileCoor.Running);

                            break;
                    }
                };

                OperationFailedException.Invoke(this, handler);
            }
        }

        public event EventHandler<FileOperationErrorHandler<FileExistsRecoveryOption>>? FileExistsException;
        public void OnFileExistsException(FileOperation operation, string errorMessage)
        {
            if (FileExistsException is null) // no subscribers to handle error
            {
                _stateMachine.ChangeState(FileCoor.Aborted);
            }
            else
            {
                FileOperationErrorHandler<FileExistsRecoveryOption> handler = new(operation, errorMessage);

                handler.RecoveryOptionSelected += (s, e) =>
                {
                    switch (e)
                    {
                        default: // Abort

                            ChangeState(FileCoor.Aborted);

                            break;

                        case FileExistsRecoveryOption.Overwrite:

                            operation.OverwriteAll = true;
                            operation.Reset();

                            ChangeState(FileCoor.Running);

                            break;

                        case FileExistsRecoveryOption.OverwriteAll:

                            OverwriteAll = true;
                            operation.Reset();

                            ChangeState(FileCoor.Running);

                            break;
                    }
                };

                FileExistsException.Invoke(this, handler);
            }
        }




        private void UpdateMetricsTimer_Tick(object sender, EventArgs e) => UpdateMetrics();

        private void Add(FileOperation operation)
        {
            operation.OperationCompleted += Operation_OperationCompleted;
            operation.OperationError += Operation_OperationError;
            _operations.Add(operation);
        }

        private void Remove(FileOperation operation)
        {
            operation.OperationCompleted -= Operation_OperationCompleted;
            operation.OperationError -= Operation_OperationError;
            _operations.Remove(operation);
        }

        private void Clear()
        {
            _currentOperation = null;

            while (_operations.Count > 0) Remove(_operations.First());
        }

        public void CopyFile(string source, string destination) => Add(new FileCopyOperation(source, destination));

        public void CopyDirectory(string sourceDir, string destinationDir)
        {
            var fsEntries = Directory.GetFileSystemEntries(sourceDir, "*", SearchOption.AllDirectories);

            string DestinationPath(string entryPath) => Path.Combine(destinationDir, entryPath.Replace(sourceDir + '\\', ""));

            foreach (var directory in fsEntries.Where(x => Directory.Exists(x)))
                Add(new DirectoryCreateOperation(DestinationPath(directory))); // do this explicity so empty directories are created

            foreach (var file in fsEntries.Where(x => File.Exists(x))) 
                Add(new FileCopyOperation(file, DestinationPath(file)));

        }

        public void ExtractZipFile(string zipFile, string directory) => Add(new UnzipFileOperation(zipFile, directory));

        public void ExtractZipFile(string zipFile, string directory, string[] excludedExtensions) => 
            Add(new UnzipFileOperation(zipFile, directory) { ExcludedExtensions = excludedExtensions});

        public void DeleteDirectory(string path) => Add(new DeleteDirectoryOperation(path));

        public void MoveDirectory(string source, string destination) => Add(new DirectoryMoveOperation(source, destination));

        private void StartMetrics()
        {
            _filteredProgress.Reset();
            _elapsedTime.Start();
            _updateMetricsTimer.Enabled = true;
        }

        private void PauseMetrics()
        {
            _elapsedTime.Stop();
            _updateMetricsTimer.Enabled = false;
            UpdateMetrics();
        }

        // for smoothing ETA & MegaBytesPerSecond
        private readonly Filters.MovingAverage _filteredProgress = new() { Length = 5 };

        private void UpdateMetrics()
        {
            TotalBytes = _operations.Select(op => op.TotalBytes).Sum() + 1;

            Progress = (double)_operations.Select(op => op.CompletedBytes).Sum() / TotalBytes;

            _filteredProgress.InputValue = Progress;

            if (_filteredProgress.OutputValue != 0) // so no divide by 0
            {
                EstimatedCompleteTimeSpan = new TimeSpan((long)(_elapsedTime.ElapsedTicks / _filteredProgress.OutputValue) - _elapsedTime.ElapsedTicks);
                EstimatedCompleteTimeMilliseconds = (int)EstimatedCompleteTimeSpan.TotalMilliseconds;
            }

            MegaBytesPerSecond = Progress * TotalBytes / (_elapsedTime.ElapsedMilliseconds * 1048.576); // (1024^2)/1000

            ProgressUpdated?.Invoke(this, new EventArgs());
        }

        public event EventHandler ProgressUpdated;

    }
}
