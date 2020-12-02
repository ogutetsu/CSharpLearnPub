using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Task
{
    public class TaskAPMPattern : ConsoleMenu
    {
        delegate string AsyncTask(string threadName);

        delegate string IncompatibleAsyncTask(out int threadId);

        void Callback(IAsyncResult ar)
        {
            Console.WriteLine($"Start callback");
            Console.WriteLine($"State callback : {ar.AsyncState}");
            Console.WriteLine($"Thread pool : {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Thread id : {Thread.CurrentThread.ManagedThreadId}");
        }

        string Test(string threadName)
        {
            Console.WriteLine($"Start...");
            Console.WriteLine($"thread pool : {Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Thread.CurrentThread.Name = threadName;
            return $"Thread name : {Thread.CurrentThread.Name}";
        }

        string Test(out int threadId)
        {
            Console.WriteLine($"Start...");
            Console.WriteLine($"Thread pool : {Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            threadId = Thread.CurrentThread.ManagedThreadId;
            return $"Thread id : {threadId}";
        }
        
        public override void Run()
        {
            int threadId;
            AsyncTask d = Test;
            IncompatibleAsyncTask e = Test;
            
            Console.WriteLine($"Option 1");
            Task<string> task = Task<string>.Factory.FromAsync(
                d.BeginInvoke("AsyncTaskThread", Callback,
                    "delegate async call"), d.EndInvoke);
            task.ContinueWith(t =>
            {
                Console.WriteLine($"Callback finish result : {t.Result}");
            });

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            
            Console.WriteLine(task.Status);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
            Console.WriteLine("Option 2");

            task = Task<string>.Factory.FromAsync(
                d.BeginInvoke, d.EndInvoke,"AsyncTaskThread", "delegate async call"
            );

            task.ContinueWith(t =>
                Console.WriteLine($"Task comp Result : {t.Result}"));

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine($"Option 3");

            IAsyncResult ar = e.BeginInvoke(out threadId, Callback, $"delegate async call");
            task = Task<string>.Factory.FromAsync(ar, _ => e.EndInvoke(out threadId, ar));

            task.ContinueWith(t =>
                Console.WriteLine($"Task comp Result : {t.Result}, Thread id : {threadId}"));

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}