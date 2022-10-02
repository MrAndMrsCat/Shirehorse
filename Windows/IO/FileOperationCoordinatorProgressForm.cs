using System.ComponentModel;
using Shirehorse.Core.Diagnostics;
using Shirehorse.Core.DLL;
using Shirehorse.Core.Extensions;
using static Shirehorse.Core.IO.FileOperationCoordinator;

namespace Shirehorse.Core.IO
{
    public partial class FileOperationCoordinatorProgressForm : Form
    {
        public FileOperationCoordinatorProgressForm(FileOperationCoordinator coordinator)
        {
            Coordinator = coordinator;

            InitializeComponent();

            // some bug causes the form to lock up if we've never called this even if we call Show() then do some work
            // so Show() then Hide() and we're good...
            Show();

            Coordinator.ProgressUpdated += (s, e) => this.ThreadSafe(Coordinator_ProgressUpdated, s, e);

            Coordinator.CoordinatorStateChanged += (s, e) => this.ThreadSafe(Coordinator_StateChanged, s, e);

            Coordinator.OperationFailedException += (s, e) => this.ThreadSafe(Coordinator_LockedFileException, s, e);

            Coordinator.FileExistsException += (s, e) => this.ThreadSafe(Coordinator_FileExistsException, s, e);

            FormHeightAtStart = Height;
            ShowLabels(true);

            coloredProgressBar.NyanCat = true;

            // now hide again
            Hide();
        }

        public bool HideOnComplete { get; set; } = true;
        public bool HideOnAbort { get; set; } = true;

        private FileOperationCoordinator Coordinator;

        private int FormHeightAtStart;
        


        // for testing
        public double ProgressBarValue
        {
            get => coloredProgressBar.Value;
            set => coloredProgressBar.Value = value;   
        }

        public new void Show()
        {
            base.Show();
            WindowState = FormWindowState.Normal;
            User32.SetForegroundWindow(Handle);
        }

        private readonly int _hideLabelHeight = 60;
        private void ShowLabels(bool show)
        {
            Height = show
                ? FormHeightAtStart
                : FormHeightAtStart - _hideLabelHeight;

            foreach (Control ctrl in Controls)
            {
                if (ctrl is Label) ctrl.Visible = show;
            }
        }

        
        private readonly string _remainingTimeFormat = "Time remaining: ";
        private readonly string _itemsRemainingFormat = "Items Remaining: ";

        private void Coordinator_ProgressUpdated(object? sender, EventArgs e)
        {
            coloredProgressBar.Value = Math.Min(Coordinator.Progress, 1);

            lbl_status.Text = $"{Coordinator.Status}";

            lbl_timeRemaining.Text = $"{_remainingTimeFormat}{Coordinator.EstimatedCompleteTimeSpan:hh\\:mm\\:ss} ({Coordinator.MegaBytesPerSecond:0.0}MB/s)";

            lbl_itemsRemaining.Text = $"{_itemsRemainingFormat}{Coordinator.RemainingItems}";

            string caption = $"{Coordinator.OperationTypeDescription} - {100 * Coordinator.Progress:0.0}% complete";

            switch (Coordinator.CurrentState)
            {
                case FileCoor.Error:
                    caption += " - Waiting for input";
                    break;

                case FileCoor.Aborted:
                    caption += " - Aborted";
                    break;
            }

            Text = caption;
        }




        private enum LockedFileOptions
        {
            Abort,
            Retry,
            CheckForProcessLocks,
            ShowExceptionDetail,
        }

        private void Coordinator_LockedFileException(object? sender, FileOperationErrorHandler<OperationFailedRecoveryOption> errorHandler)
        {
            EnumerationDialogForm<LockedFileOptions> lockedFileDialog = new()
            {
                CloseOnSelection = false,
                Caption = "Locked File Error",
                Message = errorHandler.ErrorMessage,
            };

            lockedFileDialog.ChoiceSelected += (s, e) =>
            {
                switch (lockedFileDialog.DialogResult)
                {
                    default: // Abort

                        lockedFileDialog.Close();
                        errorHandler.OnRecoveryOptionSelected(this, OperationFailedRecoveryOption.Abort);
                        break;

                    case LockedFileOptions.Retry:

                        lockedFileDialog.Close();
                        errorHandler.OnRecoveryOptionSelected(this, OperationFailedRecoveryOption.Retry);
                        break;

                    case LockedFileOptions.CheckForProcessLocks:

                        new LockedFilesForm(errorHandler.Exception) { CloseFormOnNoLockedFiles = false };

                        break;

                    case LockedFileOptions.ShowExceptionDetail:

                        new ExceptionForm(errorHandler.Exception);

                        break;
                }
            };

        }

        private void Coordinator_FileExistsException(object? sender, FileOperationErrorHandler<FileExistsRecoveryOption> errorHandler)
        {
            EnumerationDialogForm<FileExistsRecoveryOption> overwriteDialog = new()
            {
                CloseOnSelection = true,
                Caption = "Locked File Error",
                Message = errorHandler.ErrorMessage,
            };

            overwriteDialog.ChoiceSelected += (s, e) =>
            {
                errorHandler.OnRecoveryOptionSelected(this, overwriteDialog.DialogResult);
            };
        }

        private void Coordinator_StateChanged(object? sender, CoordinatorEventArgs e)
        {
            switch (e.State)
            {
                case FileCoor.Running:
                    Show();
                    coloredProgressBar.BarColor = Color.LimeGreen;
                    break;

                case FileCoor.Error:
                    coloredProgressBar.BarColor = Color.Yellow;
                    break;

                case FileCoor.Aborted:
                    coloredProgressBar.BarColor = Color.Red;
                    if (HideOnAbort) Hide();
                    break;

                case FileCoor.Completed:
                    if (HideOnComplete) Hide();
                    break;
            }
        }

        private void FileDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Coordinator.AllCompleted)
            {
                if (MessageBox.Show( "The are operations that have not completed, close this window and abort?", "Operations have not completed", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Coordinator.Abort();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void FileOperationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Coordinator.CoordinatorStateChanged -= Coordinator_StateChanged;
        }

        private void FileOperationForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            TopMost = MessageBox.Show("Keep on top?", "Keep on top?", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
    }
}
