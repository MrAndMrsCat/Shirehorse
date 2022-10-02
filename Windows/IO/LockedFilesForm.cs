using Shirehorse.Core.Diagnostics;
using Shirehorse.Core.Diagnostics.Logging;
using Shirehorse.Core.Extensions;
using System.Data;
using System.Diagnostics;

namespace Shirehorse.Core
{
    public partial class LockedFilesForm : Form
    {
        public LockedFilesForm(Exception exception)
        {
            var message = $"Operation failed\n\n{exception.Message}";
            SetupForm(exception.Source, message);
        }

        public LockedFilesForm(string targetPath, Exception exception)
        {
            var message = $"Operation failed\n\n{exception.Message}";
            SetupForm(targetPath, message);
        }

        public LockedFilesForm(string targetPath, string message = "", bool closeOnNoLockedFiles = true)
        {
            SetupForm(targetPath, message, closeOnNoLockedFiles);
        }

        private static string HandleExecutablePath => Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "IO", "handle64.exe");
        private static int MessageWrapLineLength => 100;
        public string TargetPath { get; set; }
        public List<LockedFileInfo> LockedFiles { get; private set; } = new();
        public bool ShowOnStart { get; set; } = true;
        public bool CloseFormOnNoLockedFiles { get; set; } = true;

        public void SetupForm(string targetPath, string message, bool closeOnNoLockedFiles = true)
        {
            InitializeComponent();
            TargetPath = targetPath;
            CloseFormOnNoLockedFiles = closeOnNoLockedFiles;

            if (message != "")
            {
                var lines = message.WrapNewLine(MessageWrapLineLength).Split('\n');

                foreach (string line in lines) Tree.Nodes.Add(line);
            }

            RefreshFilesAndTree(clearTree: false);
        }

        public List<string> KillAll()
        {
            var result = new List<string>();

            foreach (var pid in LockedFiles.Select(x => x.PID).Distinct())
            {
                Process.GetProcessById(pid).Kill();
                result.Add($"Killed process: {pid}");
            }

            return result;
        }


        public void RefreshFilesAndTree(bool clearTree = true)
        {
            if (clearTree) Tree.Nodes.Clear();
            Tree.Nodes.Add($"Checking for file locks under:");
            Tree.Nodes.Add(TargetPath);
            Tree.Nodes.Add($"please wait...");

            if (ShowOnStart) Show();
            UpdateLockedFiles();
            PopulateTree();
        }


        private void PopulateTree()
        {
            Tree.Nodes.Clear();

            if (LockedFiles.Count > 0)
            {
                foreach (var group in LockedFiles.GroupBy(x => x.PID))
                {
                    var rootNode = new TreeNode() 
                    {
                        Text = group.Key.ToString(),
                        ContextMenuStrip = contextMenuStrip
                    };

                    foreach (var lockedFile in group)
                    {
                        rootNode.Text = $"{lockedFile.ProcessName} (PID: {lockedFile.PID})";
                        rootNode.Tag = lockedFile.PID;
                        rootNode.Nodes.Add(new TreeNode(lockedFile.Path));
                    }

                    Tree.Nodes.Add(rootNode);
                    rootNode.Expand();
                }
            }
            else
            {
                if (CloseFormOnNoLockedFiles) Close();

                Tree.Nodes.Add("No locked files found");
            }
        }

        public static List<LockedFileInfo> GetLockedFiles(string path)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo()
                {
                    FileName = HandleExecutablePath,
                    Arguments = "\"" + path + "\" /accepteula",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    Verb = "runas"

                });
                process.WaitForExit();

                string output = process.StandardOutput.ReadToEnd();

                return LockedFileInfo.ParseHandleOutput(output);
            }
            catch (Exception ex)
            {
                SystemHandler.Handle(ex);
                return new List<LockedFileInfo>();
            }

        }


        public void UpdateLockedFiles()
        {
            LockedFiles = GetLockedFiles(TargetPath);

            if (LockedFiles.Count > 0)
            {
                SystemLog.Log($"Handle64.exe found locked files under: {TargetPath}");

                foreach (var group in LockedFiles.GroupBy(x => x.PID))
                {
                    SystemLog.Log($"PID: { group.Key}");

                    foreach (var lockedFile in group)
                    {
                        SystemLog.Log($"\tProcessName: {lockedFile.ProcessName}, Path: { lockedFile.Path}");
                    }
                }
            }
            else
            {
                SystemLog.Log($"Handle64.exe did not detect any locked files under: {TargetPath}");
            }

            ts_killAllProcesses.Visible = LockedFiles.Count > 0;
        }

        private void KillAllProcesses_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var pid in LockedFiles.Select(x => x.PID).Distinct())
                    Process.GetProcessById(pid).Kill();
            }
            catch { }

            RefreshFilesAndTree();
        }

        // if the form loses focus the toolstrip requires two clicks to fire the clicked event, this fixes it
        protected override void WndProc(ref Message m)
        {
            const int WM_PARENTNOTIFY = 0x0210;
            if (m.Msg == WM_PARENTNOTIFY)
                if (!Focused)
                    Activate();

            base.WndProc(ref m);
        }

        private void KillProcess_Click(object sender, EventArgs e)
        {
            try
            {
                var pid = (int)Tree.SelectedNode.Tag;
                Process.GetProcessById(pid).Kill();
                Tree.SelectedNode.Remove();
            }
            catch { }
        }

        private void Refresh_Click(object sender, EventArgs e) => RefreshFilesAndTree();

        public static void ShowOpenFileDialog(string path = "")
        {
            var form = new Form()
            {
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true,
                ShowIcon = false,
                MinimizeBox = false,
                MaximizeBox = false,
                Text = "Check for locked files"
            };

            var pathControl = new FilePathUserControl()
            {
                Label = "Path",
                Path = path.Trim('\'').Trim('\"'),
                DisplayStyle = FilePathUserControl.Style.Folder,
                Width = 600
            };

            pathControl.EnterPressed += PathControl_EnterPressed;

            form.Controls.Add(pathControl);
            form.Show();
            pathControl.Select();
        }

        private static void PathControl_EnterPressed(object sender, EventArgs e)
        {
            var control = sender as FilePathUserControl;
            var path = control.Path;

            if(File.Exists(path) || Directory.Exists(path))
            {
                (control.Parent as Form).Close();
                new LockedFilesForm(path, closeOnNoLockedFiles: false);
            }
        }
    }

    public class LockedFileInfo
    {
        public static List<LockedFileInfo> ParseHandleOutput(string handleOutput)
        {
            var lines = handleOutput.Replace("\r", "").Split('\n');

            var result = new List<LockedFileInfo>();

            if (lines[5] != "No matching handles found.")
            {
                for (int i = 5; i < lines.Length - 1; i++)
                    result.Add(new LockedFileInfo(lines[i]));
            }

            return result;
        }


        public string ProcessName { get; }
        public int PID { get; }
        public string Type { get; }
        public string Path { get; }

        LockedFileInfo(string handleOutputLine)
        {
            var split = handleOutputLine.Split(' ');

            int id = 0;

            for (int i=0; i < split.Length; i++)
            {
                if (split[i].Length > 0)
                {
                    switch (id)
                    {
                        case 0:
                            ProcessName = split[i];
                            break;
                        case 2:
                            PID = int.Parse(split[i]);
                            break;
                        case 4:
                            Type = split[i];
                            break;
                    }
                    id++;
                }
            }

            split = handleOutputLine.Split(':');

            Path = (split[split.Length - 2] + ":" + split[split.Length - 1]).Trim();
        }

        public override string ToString() =>
            string.Concat(ProcessName, "PID:", PID, Type, Path);
        
    }
}
