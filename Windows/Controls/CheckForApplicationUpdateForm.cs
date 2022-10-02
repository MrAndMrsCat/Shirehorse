using System.Diagnostics;

namespace Shirehorse.Core
{
    public partial class CheckForApplicationUpdateForm : Form
    {
        public string? CurrentVersion { set; get; }
        public string? NewVersion { set; get; }
        public string? NewFilePath { set; get; }
        public string? InstallerPath { set; get; }
        public bool UpdateAvailable { private set; get; }
        public bool ForceShow { get; set; }

        private int retries = 20;
        private int retryCount = 0;
        private bool finishedCheck = false;
        private System.Timers.Timer checkTaskCompleteTimer = new () { Interval = 500, Enabled = true };

        public CheckForApplicationUpdateForm()
        {
            InitializeComponent();
            checkTaskCompleteTimer.Elapsed += CheckTaskCompleteTimer_Elapsed;
        }

        private void CheckTaskCompleteTimer_Elapsed(object? sender, EventArgs e)
        {
            if (finishedCheck || retryCount > retries)
            {
                checkTaskCompleteTimer.Enabled = false;

                if (UpdateAvailable || ForceShow)
                {
                    Text = UpdateAvailable
                        ? "Update Available"
                        : "Update not available";

                    txtBox_currentVersion.Text = CurrentVersion;

                    if (NewVersion == "" || !File.Exists(InstallerPath))
                    {
                        label_mainText.Text = "Error reaching update server";
                        button_accept.Enabled = false;
                    }

                    txtBox_newVersion.Text = NewVersion;
                    Show();
                }
                else Close();
            }
            else retryCount++;
        }

        public void  CheckForUpdate()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    NewVersion = FileVersionInfo.GetVersionInfo(NewFilePath).FileVersion;
                    UpdateAvailable = Build(CurrentVersion) < Build(NewVersion) && File.Exists(InstallerPath); 
                }
                catch { }
                
                finishedCheck = true;
            });

        }

        static int Build(string version)
        {
            int.TryParse(version.Split('.').Last(), out int build);
            return build;
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            Process.Start(InstallerPath);
            //Close();
            Application.Exit();
        }

        private void button_cancel_Click(object sender, EventArgs e) => Close();
        
    }
}
