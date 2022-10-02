using Shirehorse.Core.Diagnostics.Logging;

namespace Shirehorse.Core.Extensions
{
    public static class FileInfoExtension
    {
        public static int CopyToUpdateCounts => 1000;

        public static void CopyTo(this FileInfo file, FileInfo destination, Action<int> progressCallback)
        {
            try
            {
                const int bufferSize = 1024 * 1024;
                byte[] buffer = new byte[bufferSize], buffer2 = new byte[bufferSize];
                bool swap = false;
                int progress = 0, reportedProgress = 0, read = 0;
                long len = file.Length;
                float flen = len;
                Task writer = null;

                using var source = file.OpenRead();
                using var dest = destination.OpenWrite();

                dest.SetLength(source.Length);

                for (long size = 0; size < len; size += read)
                {
                    if ((progress = ((int)((size / flen) * CopyToUpdateCounts))) != reportedProgress)
                        progressCallback(reportedProgress = progress);
                    read = source.Read(swap ? buffer : buffer2, 0, bufferSize);
                    writer?.Wait();
                    writer = dest.WriteAsync(swap ? buffer : buffer2, 0, read);
                    swap = !swap;
                }
                writer?.Wait();
            }
            catch (Exception ex) { SystemLog.Log(ex); }
        }
    }
    
}
