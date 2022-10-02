namespace Shirehorse.Core.IO
{
    public interface IDeviceConnection
    {
        public bool Connected { get; set; }
        public void Connect();
        public void Disconnect();

        public event EventHandler ConnectionChanged;
    }
}
