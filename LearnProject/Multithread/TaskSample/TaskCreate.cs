using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.TaskSample
{
    public class TaskCreate : ConsoleMenu
    {

        void TaskMethod(string name)
        {
            Console.WriteLine($"Task {name} thread id : {Thread.CurrentThread.ManagedThreadId} pool thread : {Thread.CurrentThread.IsThreadPoolThread}");
        }
        public override void Run()
        {
            var t1 = new Task(() => TaskMethod("Task1"));
            var t2 = new Task(()=>TaskMethod("Task2"));
            t2.Start();
            t1.Start();
            Task.Run(() => TaskMethod("Task 3"));
            Task.Factory.StartNew(() => TaskMethod("Task 4"));
            Task.Factory.StartNew(() => TaskMethod("Task5"), TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(1));

        }
    }
}