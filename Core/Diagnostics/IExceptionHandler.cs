namespace Shirehorse.Core.Diagnostics
{
    public interface IExceptionHandler
    {
        void Handle(Exception exception);
    }
}
