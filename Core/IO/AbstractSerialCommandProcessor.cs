using Shirehorse.Core.FiniteStateMachines;
using Shirehorse.Core.Diagnostics.Logging;
using System.Collections.Concurrent;

namespace Shirehorse.Core.IO
{
    public abstract class AbstractSerialCommandProcessor 
    {
        public AbstractSerialCommandProcessor()
        {
            BuildFSMTransitions();
            BuildFSMStateActions();
            FSM.ChangeState(CommandProcesser.Idle);
            Logger = new(LogSourceName);
        }

        private enum CommandProcesser
        {
            Reset,
            Idle,
            CheckingBuffer,
            SendingCommand,
            SentSuccessfully,
            Timeout,
            FailedToSend,
            WaitingToRetry
        }

        private FiniteStateMachine<CommandProcesser> FSM { get; } = new();

        private void BuildFSMTransitions()
        {
            FSM[CommandProcesser.Reset, CommandProcesser.Idle] = new();
            FSM[CommandProcesser.Idle, CommandProcesser.CheckingBuffer] = new();
            FSM[CommandProcesser.CheckingBuffer, CommandProcesser.Idle] = new();
            FSM[CommandProcesser.CheckingBuffer, CommandProcesser.SendingCommand] = new();
            FSM[CommandProcesser.SendingCommand, CommandProcesser.SentSuccessfully] = new();
            FSM[CommandProcesser.SendingCommand, CommandProcesser.FailedToSend] = new();
            FSM[CommandProcesser.SentSuccessfully, CommandProcesser.CheckingBuffer] = new();
            FSM[CommandProcesser.FailedToSend, CommandProcesser.WaitingToRetry] = new();
            FSM[CommandProcesser.FailedToSend, CommandProcesser.CheckingBuffer] = new();
            FSM[CommandProcesser.WaitingToRetry, CommandProcesser.SendingCommand] = new();
        }

        private void BuildFSMStateActions()
        {
            FSM.ResetAction = () => 
            { 
                _commandQueue.Clear();
                FSM.ChangeState(CommandProcesser.Idle);
            };

            FSM[CommandProcesser.CheckingBuffer].EntryAction = () =>
            {
                if (_commandQueue.TryDequeue(out _currentCommand))
                {
                    _remainingRetries = Retries;
                    FSM.ChangeState(CommandProcesser.SendingCommand);
                }
                else
                {
                    FSM.ChangeState(CommandProcesser.Idle);
                }
            };

            FSM[CommandProcesser.SendingCommand] = new(timeout: Timeout, timeoutState: CommandProcesser.FailedToSend)
            {
                EntryAction = () =>
                {
                    try
                    {
                        if (_currentCommand is not null)
                        {
                            PrivateSendCommand(_currentCommand.Value.Command);
                        }
                        else throw new Exception("Requested send command but the buffer is empty!");
                    }
                    catch (Exception ex)
                    {
                        TriggerFailedToSend(ex);
                    }
                }
            };

            FSM[CommandProcesser.FailedToSend].EntryAction = () =>
            {
                FSM.ChangeState(_remainingRetries > 0
                    ? CommandProcesser.WaitingToRetry
                    : CommandProcesser.CheckingBuffer);
            };

            FSM[CommandProcesser.SentSuccessfully].EntryAction = () =>
            {
                _currentCommand = null;
                FSM.ChangeState(CommandProcesser.CheckingBuffer);
            };

            FSM[CommandProcesser.WaitingToRetry] = new(timeout: RetryDelay, timeoutState: CommandProcesser.SendingCommand)
            {
                EntryAction = () => { _remainingRetries--; }
            };
        }

        

        public int Timeout
        {
            get => _timeout;
            set
            {
                _timeout = value;
                FSM[CommandProcesser.SendingCommand].PollingInterval = _timeout;
            }
        }
        private int _timeout = 10000; // ms
        public int Retries { get; set; } = 3;
        public int RetryDelay
        {
            get => _retryDelay;
            set
            {
                _retryDelay = value;
                FSM[CommandProcesser.WaitingToRetry].PollingInterval = _retryDelay;
            }
        }
        private int _retryDelay = 100; // ms
        private int _remainingRetries;


        public LogSource? Logger { get; set; } 
        protected string LogSourceName => FSM.LogSourceName;




        protected void TriggerFailedToSend(Exception exception)
        {
            Logger?.Log(ILogging.Category.Error, $"Failed to send with exception: {exception.Message}");
            FSM.ChangeState(CommandProcesser.FailedToSend);
        }



        public void SendCommandWithResponseHandler(byte[] command, Action<byte[]> responseHandler)
        {
            _commandQueue.Enqueue((command, responseHandler));
            FSM.ChangeState(CommandProcesser.CheckingBuffer);
        }

        private (byte[] Command, Action<byte[]> ResponseHandler)? _currentCommand;
        private readonly ConcurrentQueue<(byte[] Command, Action<byte[]> ResponseHandler)?> _commandQueue = new();

        protected abstract void PrivateSendCommand(byte[] command);
        protected virtual void PrivateProcessCommand(byte[] response)
        {
            try
            {
                if (_currentCommand is not null)
                {
                    _currentCommand.Value.ResponseHandler(response);
                    FSM.ChangeState(CommandProcesser.SentSuccessfully);
                }
                else throw new Exception("Requested send command but the buffer is empty!");
            }
            catch (Exception ex)
            {
                TriggerFailedToSend(ex);
            }
        }
    }
}

