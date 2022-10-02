namespace Shirehorse.Core.Configuration
{
    public interface IOptionUserControl
    {
        void Initialize();
        Option Option { get; set; }
    }
}


