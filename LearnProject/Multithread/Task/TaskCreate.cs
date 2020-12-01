using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Task
{
    public class TaskCreate : ConsoleMenu
    {

        void TaskMethod(string name)
        {
            Console.WriteLine($"Task {name} thread id : {Thread.CurrentThread.ManagedThreadId} pool thread : {Thread.CurrentThread.IsThreadPoolThread}");
        }
        public override void Run()
        {
            var t1 = new System.Threading.Tasks.Task(() => TaskMethod("Task1"));
            var t2 = new System.Threading.Tasks.Task(()=>TaskMethod("Task2"));
            t2.Start();
            t1.Start();
            System.Threading.Tasks.Task.Run(() => TaskMethod("Task 3"));
            System.Threading.Tasks.Task.Factory.StartNew(() => TaskMethod("Task 4"));
            System.Threading.Tasks.Task.Factory.StartNew(() => TaskMethod("Task5"), TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(1));

        }
    }
}