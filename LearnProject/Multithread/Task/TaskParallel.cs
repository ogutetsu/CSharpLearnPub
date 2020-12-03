using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Task
{
    public class TaskParallel : ConsoleMenu
    {

        int TaskMethod(string name, int seconds)
        {
            Console.WriteLine($"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return seconds;
        }
        public override void Run()
        {
            var task1 = new Task<int>(() => TaskMethod("Task 1", 3));
            var task2 = new Task<int>(() => TaskMethod("Task 2", 2));
            var whenAllTask = System.Threading.Tasks.Task.WhenAll(task1, task2);

            whenAllTask.ContinueWith(t =>
                    Console.WriteLine($"1st result {t.Result[0]}  2nd result {t.Result[1]}"),
                TaskContinuationOptions.OnlyOnRanToCompletion
            );

            task1.Start();
            task2.Start();
            
            Thread.Sleep(TimeSpan.FromSeconds(4));
            
            var tasks = new List<Task<int>>();
            for (int i = 1; i < 4; i++)
            {
                int counter = i;
                var task = new Task<int>(() => TaskMethod($"Task {counter}", counter));
                tasks.Add(task);
                task.Start();
            }

            while (tasks.Count > 0)
            {
                var compTask = System.Threading.Tasks.Task.WhenAny(tasks).Result;
                tasks.Remove(compTask);
                Console.WriteLine($"comp result {compTask.Result}");
            }

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
        
        
    }
}