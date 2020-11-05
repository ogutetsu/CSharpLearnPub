using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadLock: ConsoleMenu
    {

        abstract class Base
        {
            public abstract void Inc();
            public abstract void Dec();
        }
        
        class Sample : Base
        {
            public int cnt { get; private set; }
            public override void Inc() => cnt++;
            public override void Dec() => cnt--;
        }

        class SampleLock : Base
        {
            public readonly object _sync = new object();
            public int cnt { get; private set; }
            public override void Inc()
            {
                lock (_sync)
                {
                    cnt++;
                }
            }

            public override void Dec()
            {
                lock (_sync)
                {
                    cnt--;
                }
            }
        }

        private void Count(Base b)
        {
            for (int i = 0; i < 10000; i++)
            {
                b.Inc();
                b.Dec();
            }
        }
        public override void Run()
        {
            var c = new Sample();
            var thread1 = new Thread(() => Count(c));
            var thread2 = new Thread(() => Count(c));
            var thread3 = new Thread(() => Count(c));

            thread1.Start();
            thread2.Start();
            thread3.Start();
            
            thread1.Join();
            thread2.Join();
            thread3.Join();
            
            Console.WriteLine($"Count : {c.cnt}");
            
            var c1 = new SampleLock();
            thread1 = new Thread(() => Count(c1));
            thread2 = new Thread(() => Count(c1));
            thread3 = new Thread(() => Count(c1));
            thread1.Start();
            thread2.Start();
            thread3.Start();
            
            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine($"Count : {c1.cnt}");
        }
    }
}