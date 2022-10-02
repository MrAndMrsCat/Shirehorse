using System.Reflection;
using Shirehorse.Core.FiniteStateMachines;
using Shirehorse.Core.Diagnostics;

namespace Shirehorse.Core
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public AboutForm(AssemblyName assembly_name)
        {
            InitializeComponent();

            var vers = assembly_name.Version;
            label_version_value.Text = $"{vers.Major}.{vers.Minor}.{vers.Build}\n{vers.Revision}";

            Show();
        }

        public string Header { set => label_header.Text = value; }
        public string App_Version { set => label_version_value.Text = value; }
        public string URL { set => linkLabel_url.Text = value; } 
        public string Contact { set => linkLabel_support_contact.Text = value; }
        public bool ShowStateMachineButton { set => btn_showStateMachines.Visible = value; }

        internal List<IStateMachine> FSMEnums { get; set; }

        private void URL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel_url.Text);
        }

        private void Contact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + linkLabel_support_contact.Text);
        }

        private void ShowStateMachines_Click(object sender, EventArgs e) /*=> ShowStateMachinesClick?.Invoke(this, new EventArgs());*/
        {
            var allForm = new FiniteStateMachineCollectionForm();
            allForm.ShowAll();
            allForm.Show();
        }


        

        //public event EventHandler ShowStateMachinesClick;

        private void ShowConsole_Click(object sender, EventArgs e)
        {
            DiagnositicPolicy.EnableConsole();
        }
    }
}
