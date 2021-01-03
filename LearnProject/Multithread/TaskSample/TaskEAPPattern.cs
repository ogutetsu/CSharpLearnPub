using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.TaskSample
{
    public class TaskEAPPattern : ConsoleMenu
    {
        int TaskMethod(string name, int seconds)
        {
            Console.WriteLine(
                $"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}"
                );
            
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return seconds;
        }
        
        public override void Run()
        {
            var ts = new TaskCompletionSource<int>();
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, eventArgs) =>
            {
                eventArgs.Result = TaskMethod("Background worker", 5);
            };
            worker.RunWorkerCompleted += (sender, eventArgs) =>
            {
                if (eventArgs.Error != null)
                {
                    ts.SetException(eventArgs.Error);
                }
                else if (eventArgs.Cancelled)
                {
                    ts.SetCanceled();
                }
                else
                {
                    ts.SetResult((int)eventArgs.Result);
                }

            };
            
            worker.RunWorkerAsync();
            int result = ts.Task.Result;
            Console.WriteLine($"Result : {result}");
        }
    }
}