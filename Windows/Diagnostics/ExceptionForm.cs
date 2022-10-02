using System.Collections;
using System.Reflection;
using System.Text;

namespace Shirehorse.Core.Diagnostics
{
    public partial class ExceptionForm : Form, IExceptionHandler
    {
        public ExceptionForm(Exception exception)
        {
            InitializeComponent();

            OuterException = exception;

            label.Text = exception.Message;

            Show();

#if DEBUG
            ShowDetail();
#endif
        }

        public ExceptionForm() { } // allow parameterless constructor for system eventhandler

        public static ExceptionForm DefaultHandler { get; } = new ExceptionForm();
        public Exception OuterException { get; }
        public Size DetailedSize { get; } = new Size(1200, 600);

        void IExceptionHandler.Handle(Exception exception) => new ExceptionForm(exception);

        private void ShowDetail()
        {
            Size = DetailedSize;
            label.Visible = false;
            button_showDetail.Visible = false;
            button_disableExceptionMessages.Visible = false;
            button_copyToClipboard.Visible = true;

            treeView.Nodes.Add(GetExceptionInfo(OuterException));

            treeView.ExpandAll();
        }


        private TreeNode GetExceptionInfo(Exception ex)
        {
            var result = new TreeNode(ex.GetType().ToString());

            try
            {
                foreach (PropertyInfo propertyInfo in ex.GetType().GetProperties())
                {
                    var childNode = new TreeNode(propertyInfo.Name);

                    object value = propertyInfo.GetValue(ex, null);

                    if (value != null)
                    {
                        switch (propertyInfo.Name)
                        {
                            case "InnerException":
                                if (value is Exception inner)
                                {
                                    childNode.Nodes.Add(GetExceptionInfo(inner));
                                }

                                break;

                            case "Data":
                                foreach (DictionaryEntry kv in ex.Data)
                                {
                                    childNode.Nodes.Add($"{kv.Key} | {kv.Value}");
                                }

                                break;

                            case "StackTrace":
                                foreach (string line in value.ToString().Split('\n'))
                                {
                                    childNode.Nodes.Add(line
                                        .Replace("\r", "") // for copy to clipboard, get rid of this
                                        .Substring(6)); // trim "  at "
                                }
                                break;

                            default:
                                childNode.Nodes.Add(value.ToString());
                                break;
                        }
                    }

                    if (childNode.Nodes.Count > 0) result.Nodes.Add(childNode);
                }
            }
            catch(Exception reflectionEx)
            {
                // should not reach here
                MessageBox.Show($"Failed to get exception properties with error: {reflectionEx.Message}");
            }

            return result;
        }

        private void CopyToClipboard()
        {
            var sb = new StringBuilder();

            void GetNodeText(TreeNode node, int level)
            {
                for (int i = 0; i < level; i++) sb.Append("\t");

                sb.AppendLine(node.Text);

                foreach (TreeNode childNode in node.Nodes) GetNodeText(childNode, level + 1);
            }

            try
            {
                GetNodeText(treeView.Nodes[0], 0);
                Clipboard.SetText(sb.ToString());
            }
            catch { }
        }

        private void ShowDetail_Click(object sender, EventArgs e) => ShowDetail();

        private void CopyToClipboard_Click(object sender, EventArgs e) => CopyToClipboard();

        private void DisableExceptionMessages_Click(object sender, EventArgs e)
        {
            DiagnositicPolicy.DisableExceptionWindow();
            Close();
        }
    }
}
