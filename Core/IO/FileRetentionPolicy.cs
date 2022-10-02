using Shirehorse.Core.Diagnostics;


namespace Shirehorse.Core.IO
{
    public class FileRetentionPolicy
    {
        public FileRetentionPolicy()
        {
            _timer.Elapsed += (s,e) => Delete();
        }

        public string DirectoryPath { get; set; } = "";
        public bool Enabled { get; set; }
        public double ExpirationTimeDays { get; set; } = 7;

        private readonly System.Timers.Timer _timer = new() { Interval = 60000 };

        public void Delete()
        {
            try
            {
                if (Directory.Exists(DirectoryPath))
                {
                    foreach (string filepath in Directory.EnumerateFiles(DirectoryPath))
                    {
                        
                    }
                }
            }
            catch(Exception ex)
            {
                SystemHandler.Handle(ex);
            }
        }
    }
}
