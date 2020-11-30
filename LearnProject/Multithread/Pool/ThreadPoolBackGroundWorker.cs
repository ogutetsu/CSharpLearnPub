using System;
using System.ComponentModel;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
    public class ThreadPoolBackGroundWorker : ConsoleMenu
    {

        void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine($"Dowork thread id : {Thread.CurrentThread.ManagedThreadId}");
            var bw = (BackgroundWorker) sender;
            for (int i = 1; i <= 100; i++)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (i % 10 == 0)
                {
                    bw.ReportProgress(i);
                }
                
                Thread.Sleep(TimeSpan.FromSeconds(0.1));
            }

            e.Result = 42;

        }

        void WorkerProgress(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine($"{e.ProgressPercentage}% comp.  Progress thread id : {Thread.CurrentThread.ManagedThreadId}");
        }

        void WorkerComp(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine($"Comp thread id : {Thread.CurrentThread.ManagedThreadId}");
            if (e.Error != null)
            {
                Console.WriteLine($"Exception {e.Error.Message}");
            }
            else if (e.Cancelled)
            {
                Console.WriteLine($"Canceled");
            }
            else
            {
                Console.WriteLine($"{e.Result}");
            }
        }
        
        public override void Run()
        {
            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += WorkerDoWork;
            bw.ProgressChanged += WorkerProgress;
            bw.RunWorkerCompleted += WorkerComp;
            
            bw.RunWorkerAsync();
            
            Console.WriteLine("Press C");

            do
            {
                if (Console.ReadKey(true).KeyChar == 'C')
                {
                    bw.CancelAsync();
                }
            } while (bw.IsBusy);
        }
    }
}