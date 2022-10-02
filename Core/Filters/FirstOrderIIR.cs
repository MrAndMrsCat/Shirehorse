namespace Shirehorse.Core.Filters
{
    public class FirstOrderIIR : AbstractFilter<double>
    {
        public double Alpha 
        {
            get => _alpha;
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Cannot be < 0 or > 1");
                }

                _alpha = value;
            }
        }
        private double _alpha;
        protected override double Filter(double input)
        {
            return input * Alpha + (1 - Alpha) * OutputValue;
        }

        public override void Reset()
        {
            OutputValue = 0;
        }
    }
}
