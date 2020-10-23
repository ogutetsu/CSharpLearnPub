using System;
using LearnLib;
using System.Threading;

namespace LearnProject.Multithread
{
    public class ThreadPause : ConsoleMenu
    {
        public override void Run()
        {
            Console.WriteLine($"スレッドの一時停止");
            
            Thread t = new Thread(PrintSleep);
            t.Start();
            t.Join();
            
            Console.WriteLine($"Thread completed");
        }

        private void PrintSleep()
        {
            Console.WriteLine($"Start...");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
            
        }
    }
}