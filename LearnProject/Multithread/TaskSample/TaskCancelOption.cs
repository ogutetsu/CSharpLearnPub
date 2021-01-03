using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.TaskSample
{
    public class TaskCancelOption : ConsoleMenu
    {

        int TaskMethod(string name, int seconds, CancellationToken token)
        {
            Console.WriteLine($"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}");
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (token.IsCancellationRequested) return -1;
            }

            return seconds;
        }
        public override void Run()
        {
            var token = new CancellationTokenSource();
            var longTask = new Task<int>(() => TaskMethod("Task 1", 10, token.Token), token.Token);
            Console.WriteLine(longTask.Status);
            token.Cancel();
            Console.WriteLine(longTask.Status);
            Console.WriteLine($"Task1 cancelled");
            
            token = new CancellationTokenSource();
            longTask = new Task<int>(() => TaskMethod("Task 2", 10, token.Token), token.Token);
            longTask.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            token.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            
            Console.WriteLine($"Task comp result : {longTask.Result}");
        }
    }
}