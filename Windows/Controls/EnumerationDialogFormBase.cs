namespace Shirehorse.Core
{
    public partial class EnumerationDialogForm : Form
    {
        public EnumerationDialogForm() => InitializeComponent();

        public string Caption
        {
            get => Text;
            set => Text = value;
        }

        public string Message
        {
            get => label_message.Text;
            set => label_message.Text = value;
        }
    }



}
