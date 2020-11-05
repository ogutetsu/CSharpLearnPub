using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadParameters : ConsoleMenu
    {
        void Count(object it)
        {
            CountNums((int) it);
        }

        void CountNums(int it)
        {
            for (int i = 0; i <= it; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5f));
                Console.WriteLine($"{Thread.CurrentThread.Name} : {i}");
            }
        }

        void PrintNum(int num)
        {
            Console.WriteLine($"{num}");
        }
        
        
        class Sample
        {
            private int _num;
            public Sample(int num)
            {
                _num = num;
            }

            public void Count()
            {
                for (int i = 0; i < _num; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5f));
                    Console.WriteLine($"{Thread.CurrentThread.Name} : {i}");
                }
            }
        }
        public override void Run()
        {
            var sample = new Sample(3);
            var thread1 = new Thread(sample.Count);
            thread1.Name = "Thread1";
            thread1.Start();
            thread1.Join();
            
            Console.WriteLine($"------------------------");
            
            var thread2 = new Thread(Count);
            thread2.Name = "Thread2";
            thread2.Start(5);
            thread2.Join();
            Console.WriteLine($"------------------------");

            var thread3 = new Thread(() => CountNums(4));
            thread3.Name = "Thread3";
            thread3.Start();
            thread3.Join();
            
            Console.WriteLine($"------------------------");
            int num = 10;
            var num1 = num;
            var thread4 = new Thread(() => PrintNum(num1));
            thread4.Start();
            num = 20;
            var thread5 = new Thread( () => PrintNum(num));
            
            thread5.Start();

            thread4.Join();
            thread5.Join();
        }
    }
}