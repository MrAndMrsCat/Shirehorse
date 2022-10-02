using System.Text;
using Shirehorse.Core.Configuration;

namespace Shirehorse.Core
{
    public partial class FilePathUserControl : UserControl
    {
        public FilePathUserControl()
        {
            InitializeComponent();
            IncludeAllFiles = true;
            HighLightInvalidPath = true;
            DisplayStyle = Style.File;
            comboBox.TextChanged += (s, e) => PathChanged?.Invoke(s, e);
        }
        public enum Style
        {
            File,
            Folder,
        }
        public enum DialogType
        {
            Open,
            Save,
        }

        public string[] FileExtensions { get; set; }
        public bool IncludeAllFiles { get; set; }
        public bool HighLightInvalidPath { get; set; }
        public string Path
        {
            get => comboBox.Text; 
            set => comboBox.Text = value; 
        }

        public string Label
        {
            get => label.Text; 
            set 
            { 
                label.Text = value;
                SetInputBoxDimensions();
            }
        }

        public string[] Items
        {
            get
            {
                var result = new List<string>();
                foreach (object item in comboBox.Items) result.Add(item.ToString());
                return result.ToArray();
            }
            set
            {
                comboBox.Items.Clear();
                comboBox.Items.AddRange(value);

                if (value.Length > 0)
                {
                    comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox.Text = comboBox.Items[0].ToString();
                }

                RecentItems?.Clear();
                RecentItems?.AddRange(value);
            }
        }

        public Style DisplayStyle 
        {
            get => _displayStyle; 
            set
            {
                _displayStyle = value;

                btn_file.ImageKey = value.ToString();

                btn_file.Click -= File_Click;
                btn_file.Click -= Folder_Click;

                switch (_displayStyle)
                {
                    case Style.File:
                        btn_file.Click += File_Click;
                        break;

                    case Style.Folder:
                        
                        btn_file.Click += Folder_Click;
                        break;
                }
            } 
        }
        private Style _displayStyle;

        public DialogType Dialog { get; set; }

        public RecentList RecentItems 
        {
            get => _recentItems;
            set
            {
                _recentItems = value;

                if (_recentItems != null) Items = _recentItems.Items;
            }
        }
        private RecentList _recentItems;

        public int DropDownWidth
        {
            get => comboBox.DropDownWidth;
            set => comboBox.DropDownWidth = value;
        }

        private void SetInputBoxDimensions()
        {
            comboBox.Left = label.Width + 6;
            comboBox.Width = Width - comboBox.Left - 30;
        }

        public void AddCurrentItem() => AddItem(comboBox.Text);

        public void AddItem(string item)
        {
            comboBox.Items.Add(item);

            if (RecentItems != null)
            {
                RecentItems?.Add(item);
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void File_Click(object sender, EventArgs e)
        {
            FileDialog dialog;

            if (Dialog == DialogType.Open) 
                dialog = new OpenFileDialog(); 
            else 
                dialog = new SaveFileDialog();

            using (dialog)
            {
                dialog.Filter = FileFiltersFormatted();
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    comboBox.Text = dialog.FileName;
                    AddItem(comboBox.Text);
                    DialogClosed?.Invoke(this, new EventArgs());
                }
            }
        }

        private string FileFiltersFormatted()
        {
            var sb = new StringBuilder();

            if (FileExtensions is null) FileExtensions = new string[0];

            if (FileExtensions.Length > 1)
            {
                sb.Append("All valid files (");

                foreach (string ext in FileExtensions) sb.Append($"*.{ext};");
                sb.Length -= 1;
                sb.Append(")|");

                foreach (string ext in FileExtensions) sb.Append($"*.{ext};");
                sb.Length -= 1;
                sb.Append("|");
            }

            if (FileExtensions.Length > 0)
            {
                foreach (string ext in FileExtensions) sb.Append($"{ext} files (*.{ext})|*.{ext}|");
                sb.Length -= 1;
            }

            if (IncludeAllFiles || FileExtensions.Length == 0) 
            {
                if (sb.Length > 0) sb.Append("|");
                sb.Append("All files (*.*)|*.*");
            }
            
            return sb.ToString();
        }

        private void Folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog openFolder = new FolderBrowserDialog())
            {
                openFolder.SelectedPath = comboBox.Text;
                if (openFolder.ShowDialog() == DialogResult.OK)
                {
                    comboBox.Text = openFolder.SelectedPath;
                    AddCurrentItem();
                    DialogClosed?.Invoke(this, new EventArgs());
                }
            }
        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {
            bool validPath = false;

            switch (_displayStyle)
            {
                case Style.File:
                    validPath = File.Exists(comboBox.Text);
                    break;

                case Style.Folder:
                    validPath = Directory.Exists(comboBox.Text);
                    break;
            }

            comboBox.ForeColor = validPath
                ? SystemColors.WindowText
                : Color.Red;
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnterPressed?.Invoke(this, new EventArgs());
                AddCurrentItem();
            }  
        }

        public event EventHandler EnterPressed;
        public event EventHandler PathChanged;
        public event EventHandler DialogClosed;

        private void InputBox_SelectedValueChanged(object sender, EventArgs e) => RecentItems?.Add(comboBox.Text); // push to top of list
    }
}
