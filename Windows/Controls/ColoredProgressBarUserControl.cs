namespace Shirehorse.Core.Controls
{
    public partial class ColoredProgressBarUserControl : UserControl
    {
        public ColoredProgressBarUserControl()
        {
            InitializeComponent();
        }

        public double Value
        {
            get => _value;
            set
            {
                if (value > Maximum) throw new ArgumentOutOfRangeException($"Value {value} exceeds maximum value {Maximum}");

                _value = value;
                splitContainer.SplitterDistance = (int)(splitContainer.Width * value / Maximum);
            }
        }

        private double _value;

        public double Maximum { get; set; } = 1;

        public Color BarColor
        {
            get => splitContainer.Panel1.BackColor;
            set => splitContainer.Panel1.BackColor = value;
        }

        public bool NyanCat
        {
            get => nyanCatImage.Visible;
            set
            {
                nyanCatImage.Visible = value;
                BarColor = nyanCatImage.Visible ? SystemColors.ControlDark : Color.LimeGreen;
            }
        }
        
    }
}
