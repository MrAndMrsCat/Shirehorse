namespace Shirehorse.Core.Diagnostics.Logging
{
    public interface ILogging
    {
        public enum Category
        {
            ///<summary>Highest verbosity, only for development</summary>
            Debug = 0,

            ///<summary>High verbosity, mainly for development</summary>
            StateMachine = 1,

            ///<summary>Standard log level, general events</summary>
            Information = 2,

            ///<summary>Standard log level, only user events</summary>
            UserInput = 3,

            ///<summary>User notifications, non-critical errors e.g. invalid input, or soft tolerances</summary>
            Warning = 4,

            ///<summary>Handled exceptions, e.g. file system exception with error recovery, or hard tolerances</summary>
            Error = 5,

            ///<summary>Unhandled exceptions, should not occur in release builds</summary>
            Exception = 6,
        }

        [Flags]
        public enum Fields
        {
            All = Timestamp | Source | Category,
            Timestamp = 1,
            Source = 2,
            Category = 4,
        }

        //public virtual string LogSourceName => "General";

        //public virtual void Log(string message) => Log(Category.Debug, LogSourceName, message);
        //public virtual void Log(Exception exception) => Log(Category.Exception, LogSourceName, exception);
        //public void Log(Category category, string sourceName, string message);
        //public virtual void Log(Category category, string sourceName, Exception exception) => Log(Category.Exception, LogSourceName, exception.Message);
        

    }
}
