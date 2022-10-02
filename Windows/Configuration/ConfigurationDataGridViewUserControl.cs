using System.Data;
using System.Reflection;
using Shirehorse.Core.Diagnostics;
using Shirehorse.Core.Diagnostics.Logging;

namespace Shirehorse.Core.Configuration
{
    public partial class ConfigurationDataGridViewUserControl : UserControl
    {
        public Config Config;
        public BindingSource Option_Table = new ();

        protected DataGridView DataGridView => Grid;
        protected Type _dataType;

        public ConfigurationDataGridViewUserControl()
        {
            InitializeComponent();
            BackColor = SystemColors.Control;
            Dock = DockStyle.Fill;
        }

        public void Initialize()
        {
            try
            {
                _dataType = Config.Options.First().Value.GetType();

                // add the options to a data source

                foreach (Option option in Config.Options)
                {
                    Option_Table.Add(option.Value);
                }

                var webColors = Enum.GetValues(typeof(KnownColor))
                    .Cast<KnownColor>()
                    .Where(k => k >= KnownColor.Transparent && k < KnownColor.ButtonFace) //Exclude system colors
                    .Select(k => Color.FromKnownColor(k).ToKnownColor());

                // create columns based on the value types
                DataGridViewColumn col;
                object data_line = Config.Options.First().Value; // get any of these, they all have to have the same schema
                foreach (PropertyInfo property in _dataType.GetProperties())
                {
                    if (property.PropertyType.Equals(typeof(KnownColor)))
                    {
                        var ccol = new DataGridViewComboBoxColumn() { DataSource = Enum.GetValues(typeof(KnownColor)) };
                        Grid.Columns.Add(ccol);
                    }
                    else
                    if (property.PropertyType.IsEnum)
                    {
                        Grid.Columns.Add(new DataGridViewComboBoxColumn() { DataSource = Enum.GetValues(property.PropertyType) });
                    }
                    else
                    {
                        switch (property.GetValue(data_line)) // we only care about the types of the properties in this object
                        {
                            case bool _:
                                col = new DataGridViewCheckBoxColumn();
                                break;

                            default:
                                col = new DataGridViewTextBoxColumn();
                                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                break;
                        }
                        Grid.Columns.Add(col);
                    }

                    var c = Grid.Columns[Grid.Columns.Count - 1];
                    c.DataPropertyName = property.Name;
                    c.HeaderText = property.Name;
                    c.Name = property.Name;

                }
                Grid.DataSource = Option_Table;

                Grid.CellValueChanged += Grid_ValueChanged;
                Grid.DataError += Grid_DataError;
            }
            catch (Exception ex)
            {
                SystemHandler.Handle(ex);
            }



        }

        private void Grid_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            // Do nothing
            //throw new NotImplementedException();
        }

        private void Grid_ValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            var data = Grid.Rows[e.RowIndex].DataBoundItem;
            
            foreach (Option opt in Config.Options) // get the option that owns this, and call method that saves it
            {
                if (data.Equals(opt.Value)) 
                {
                    opt.Value = data;
                    break;
                }
            }
        }

        public void Add_New(object sender, EventArgs e)
        {
            if (_dataType is null)
            {
                SystemLog.Log(ILogging.Category.Error, "ConfigurationDataGridViewUserControl is not initialized");
            }
            else
            {
                Add(_dataType.Assembly.CreateInstance(_dataType.FullName));
            }

            
        }

        public void Add(object data)
        {
            Option opt = new () { Value = data };
            Config.Add(opt);
            Option_Table.Add(data);

        }


        public void Remove_Selected(object sender, EventArgs e)
        {
            var cell = Grid.SelectedCells;
            if (cell.Count > 0)
            {
                var data = Grid.Rows[cell[0].RowIndex].DataBoundItem;

                foreach (Option opt in Config.Options) // get the option that owns this, and call method that saves it
                {
                    if (data.Equals(opt.Value))
                    {
                        Option_Table.Remove(data);
                        Config.Remove(opt);
                        break;
                    }
                }
            }
        }

        public virtual void DataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) { }

    }


}
