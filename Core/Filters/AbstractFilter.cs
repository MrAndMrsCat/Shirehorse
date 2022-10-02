namespace Shirehorse.Core.Filters
{
    public abstract class AbstractFilter<T>
    {
        public T? InputValue 
        {
            set => SetInputValue(value);
            get => _inputValue;
        }
        private T? _inputValue;
        public T? OutputValue { get; protected set; }
        public AbstractFilter<T>? OutputFilter { set; get; }

        private void SetInputValue(T? value)
        {
            ArgumentNullException.ThrowIfNull(value);

            _inputValue = value;

            OutputValue = Filter(value);

            if (OutputFilter is not null)
            {
                OutputFilter.InputValue = OutputValue;
            }
        }

        protected abstract T Filter(T input);
        public abstract void Reset();
    }
}
