namespace Shirehorse.Core.Filters
{
    public class MovingAverage : AbstractFilter<double>
    {
        public uint Length
        {
            get => _length;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Cannot be < 1");
                }

                _length = value;
            }
        }
        private uint _length = 1;

        private readonly List<double> _values = new();

        protected override double Filter(double input)
        {
            _values.Add(input);

            while (_values.Count > _length) _values.RemoveAt(0);

            return _values.Average();
        }

        public override void Reset()
        {
            _values.Clear();
        }
    }
}
