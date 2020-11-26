using System;
using System.Diagnostics;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
	public class ThreadPoolParallel : ConsoleMenu
	{

		void UseThread(int number)
		{
			using (var countdown = new CountdownEvent(number))
			{
				Console.WriteLine($"Create thread");
				for (int i = 0; i < number; i++)
				{
					var t = new Thread(() =>
					{
						Console.Write($"{Thread.CurrentThread.ManagedThreadId}");
						Thread.Sleep(TimeSpan.FromSeconds(0.1));
						countdown.Signal();
					});
					t.Start();
				}

				countdown.Wait();
				Console.WriteLine();
			}
		}

		void UseThreadPool(int number)
		{
			using (var countdown = new CountdownEvent(number))
			{
				Console.WriteLine($"Create thread");
				for (int i = 0; i < number; i++)
				{
					ThreadPool.QueueUserWorkItem(_ =>
					{
						Console.Write($"{Thread.CurrentThread.ManagedThreadId}");
						Thread.Sleep(TimeSpan.FromSeconds(0.1));
						countdown.Signal();
					});
				}

				countdown.Wait();
				Console.WriteLine();
			}
		}
		
		public override void Run()
		{
			int count = 500;
			var sw = new Stopwatch();
			sw.Start();
			UseThread(count);
			sw.Stop();
			Console.WriteLine($"UseThread : {sw.ElapsedMilliseconds}");
			
			sw.Reset();
			sw.Start();
			UseThreadPool(count);
			sw.Stop();
			Console.WriteLine($"UseThreadPool : {sw.ElapsedMilliseconds}");

			
		}
	}
}