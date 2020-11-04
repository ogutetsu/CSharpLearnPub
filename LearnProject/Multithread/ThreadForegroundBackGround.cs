using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadForegroundBackGround : ConsoleMenu
    {

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
            var foreGround = new Sample(10);
            var backGround = new Sample(20);
            
            var thread1 = new Thread(foreGround.Count);
            thread1.Name = "ForeGround";
            var thread2 = new Thread(backGround.Count);
            thread2.Name = "BackGround";
            thread2.IsBackground = true;
            
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

        }
    }
}