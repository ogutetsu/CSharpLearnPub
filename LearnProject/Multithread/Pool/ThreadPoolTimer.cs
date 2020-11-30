
using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
    public class ThreadPoolTimer : ConsoleMenu
    {
        private Timer _timer;

        void TimerOp(DateTime start)
        {
            TimeSpan elapsed = DateTime.Now - start;
            Console.WriteLine($"{elapsed.Seconds} seconds from {start} thread id : {Thread.CurrentThread.ManagedThreadId}");
        }
        
        public override void Run()
        {
            Console.WriteLine($"Press Enter...");
            DateTime start = DateTime.Now;
            _timer = new Timer(_ => TimerOp(start), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            try
            {
                Thread.Sleep(TimeSpan.FromSeconds(6));
                _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4));
                Console.ReadLine();
            }
            finally
            {
                _timer.Dispose();
            }
            
        }
    }
}