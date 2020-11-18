using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadCountDownEvent : ConsoleMenu
    {
        
        static CountdownEvent _countdownEvent = new CountdownEvent(2);

        static void Sample(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countdownEvent.Signal();
        }
        public override void Run()
        {
            Console.WriteLine($"Start");
            var t1 = new Thread(() => Sample($"Thread1 Comp", 4));
            var t2 = new Thread(() => Sample($"Thread2 comp", 8));
            t1.Start();
            t2.Start();
            _countdownEvent.Wait();
            Console.WriteLine($"all comp");
            _countdownEvent.Dispose();

        }
    }
}