using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadManualResetEventSlim : ConsoleMenu
	{
		static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);

		static void Sample(string threadName, int seconds)
		{
			Console.WriteLine($"{threadName} sleep");
			Thread.Sleep(TimeSpan.FromSeconds(seconds));
			Console.WriteLine($"{threadName} start");
			_mainEvent.Wait();
			Console.WriteLine($"{threadName} ok");
		}
		
		public override void Run()
		{
			var t1 = new Thread(() => Sample("Thread1", 5));
			var t2 = new Thread(() => Sample("Thread2", 6));
			var t3 = new Thread(() => Sample("Thread3", 12));
			
			t1.Start();
			t2.Start();
			t3.Start();

			Thread.Sleep(TimeSpan.FromSeconds(6));
			Console.WriteLine($"sample ok");
			_mainEvent.Set();
			Thread.Sleep(TimeSpan.FromSeconds(2));
			_mainEvent.Reset();
			Console.WriteLine($"comp");
			Thread.Sleep(TimeSpan.FromSeconds(10));
			Console.WriteLine($"second");
			_mainEvent.Set();
			Thread.Sleep(TimeSpan.FromSeconds(2));
			Console.WriteLine($"comp");
			_mainEvent.Reset();

			t1.Join();
			t2.Join();
			t3.Join();

		}
	}
}