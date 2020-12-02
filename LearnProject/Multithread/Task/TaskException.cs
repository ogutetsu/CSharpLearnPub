using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Task
{
    public class TaskException : ConsoleMenu
    {

        int TaskMethod(string name, int seconds)
        {
            Console.WriteLine($"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            throw new Exception($"Exception!");
            return seconds;
        }
    
        
        public override void Run()
        {
            Task<int> task;
            try
            {
                task = System.Threading.Tasks.Task.Run(() => TaskMethod("Task 1", 2));
                int result = task.Result;
                Console.WriteLine($"Result : {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e}");
            }
            Console.WriteLine("------------------");
            Console.WriteLine();

            try
            {
                task = System.Threading.Tasks.Task.Run(() => TaskMethod("Task 2", 2));
                int result = task.GetAwaiter().GetResult();
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception : {e}");
            }
            Console.WriteLine("-----------------");
            Console.WriteLine();

        }
    }
}