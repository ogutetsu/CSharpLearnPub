using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
    public class ThreadAbort : ConsoleMenu
    {
        public override void Run()
        {
            Thread t = new Thread(PrintSleep);
            
            t.Start();
            
            Thread.Sleep(TimeSpan.FromSeconds(4));
            
            //Abortは、.NET5以降ではPlatformNotSupportedExceptionが投げられます。
            //t.Abort();
            t.Interrupt();
            Console.WriteLine($"Thread abort");

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