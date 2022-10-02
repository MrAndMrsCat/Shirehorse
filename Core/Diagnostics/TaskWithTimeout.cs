namespace Shirehorse.Core.Diagnostics
{
    public class TaskWithTimeout
    {
        public static void Invoke(int milliseconds, Action action)
        {
            var task = new Task(action);

            task.Start();

            Thread.Sleep(milliseconds);

            if (!task.IsCompleted) throw new TimeoutException($"Task timed out after {milliseconds} milliseconds");
        }

        public static void Invoke(int milliseconds, Task task)
        {
            task.Start();

            Thread.Sleep(milliseconds);

            if (!task.IsCompleted) throw new TimeoutException($"Task timed out after {milliseconds} milliseconds");
        }
    }
}
