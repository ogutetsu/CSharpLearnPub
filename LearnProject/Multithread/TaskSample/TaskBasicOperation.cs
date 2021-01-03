using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.TaskSample
{
    public class TaskBasicOperation : ConsoleMenu
    {
        Task<int> CreateTask(string name)
        {
            return new Task<int>(()=>TaskMethod(name));
        }

        private int TaskMethod(string name)
        {
            Console.WriteLine($"Task {name} thread id : {Thread.CurrentThread.ManagedThreadId} thread pool : {Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 1;
        }

        public override void Run()
        {
            TaskMethod("Main Thread Task");
            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result;
            Console.WriteLine($"Result is : {result}");

            task = CreateTask("Task 2");
            task.RunSynchronously();
            result = task.Result;
            Console.WriteLine($"Result is : {result}");

            task = CreateTask("Task 3");
            Console.WriteLine(task.Status);
            task.Start();

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            
            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine($"Result is : {result}");

        }
    }
}