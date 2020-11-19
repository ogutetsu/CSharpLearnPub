using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadSpinWait : ConsoleMenu
    {
        private static volatile bool _isComp = false;

        static void Wait()
        {
            while (!_isComp)
            {
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine($"Wait comp");
        }

        static void SpinWait()
        {
            var w = new SpinWait();
            while (!_isComp)
            {
                w.SpinOnce();
                Console.WriteLine($"Wait comp");
            }
        }
        public override void Run()
        {
            var t1 = new Thread(Wait);
            var t2 = new Thread(SpinWait);
            Console.WriteLine($"Running");
            t1.Start();
            Thread.Sleep(20);
            _isComp = true;
            Thread.Sleep(TimeSpan.FromSeconds(1));
            _isComp = false;
            Console.WriteLine($"Spinwait");
            t2.Start();
            Thread.Sleep(5);
            _isComp = true;

            t1.Join();
            t2.Join();
        }
    }
}