using Shirehorse.Core.FiniteStateMachines;
using Shirehorse.Core.Diagnostics.Logging;
using Shirehorse.Core.Extensions;

namespace Shirehorse.Core.IO
{
    public class DeviceConnectionPolicy
    {
        public DeviceConnectionPolicy(IDeviceConnection deviceConnection)
        {
            DeviceConnection = deviceConnection;
            _timeoutTimer.Elapsed += Timeout_Elapsed;

            BuildStateMachine();
            BuildFSMTransitions();

            Log = new(LogSourceName); 
        }

        public enum ConnectionPolicy
        {
            Reset,
            Disconnected,
            Connecting,
            Connected,
            CheckConnection,
        }

        public LogSource Log { get; set; }
        protected string LogSourceName => _stateMachine.LogSourceName;

        public IDeviceConnection DeviceConnection { get; private set; }
        public int ConnectRetries { get; set; } = 3;
        public double ConnectionTimeout 
        {
            get => _timeoutTimer.Interval;
            set => _timeoutTimer.Interval = value;  
        }
        private FiniteStateMachine<ConnectionPolicy> _stateMachine => new();
        private System.Timers.Timer _timeoutTimer = new() { Interval = 30000 };

        


        private void BuildStateMachine()
        {
            _stateMachine[ConnectionPolicy.Reset, ConnectionPolicy.Connecting] = new(OnConnecting);

            //StateMachine.StateChanged += StateMachine_StateChanged;
        }

        private void BuildFSMTransitions()
        {
            _stateMachine[ConnectionPolicy.Reset, ConnectionPolicy.Connecting] = new(() => 
            {
                Log.Log("my message");
                // some method
                // antoher method
                var bytes = new byte[] { 0x12, 0x01, 0xFF };

                bytes.ToString(ByteArrayExtensions.UnprintableCharacterOptions.HexString);

            });

            _stateMachine[ConnectionPolicy.Connecting, ConnectionPolicy.Connected] = new(() =>
            {
                Log.Log("my message");
            });
        }

        private void ChangeState(ConnectionPolicy state) => _stateMachine.ChangeState(state);

        //public void Start() => StateMachine.ChangeState(FileOp.Running);
        //public virtual void Abort() => StateMachine.ChangeState(FileOp.Aborted);

        public void Reset()
        {
            _stateMachine.Reset();
        }


        private void OnConnecting()
        {

        }

        private bool OnConnected() => DeviceConnection.Connected;

        private void OnCheckConnection()
        {

        }

        private void Timeout_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!DeviceConnection.Connected)
            {

            }
        }

    }
}
