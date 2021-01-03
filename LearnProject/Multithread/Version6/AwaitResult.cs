using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;


namespace LearnProject.Multithread.Version6
{
    public class AwaitResult : ConsoleMenu
    {
        Task AsyncWithTPL()
        {
            Task<string> t = GetInfoAsync("Task 1");
            Task t2 = t.ContinueWith(task => Console.WriteLine(t.Result),
                TaskContinuationOptions.NotOnFaulted);
            var t3 = t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException),
                TaskContinuationOptions.OnlyOnFaulted);
            return Task.WhenAny(t2, t3);

        }

        async Task AsyncWithAwait()
        {
            try
            {
                string result = await GetInfoAsync("Task 2");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        async Task<string> GetInfoAsync(string name)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return
                $"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}";
        }
        
        public override void Run()
        {
            Task t = AsyncWithTPL();
            t.Wait();

            t = AsyncWithAwait();
            t.Wait();

        }
    }
}