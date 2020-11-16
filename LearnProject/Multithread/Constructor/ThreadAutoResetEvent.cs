using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadAutoResetEvent : ConsoleMenu
	{
		
		static AutoResetEvent _workerEvent = new AutoResetEvent(false);
		static AutoResetEvent _mainEvent = new AutoResetEvent(false);

		static void Process(int seconds)
		{
			Console.WriteLine($"Start work");
			Thread.Sleep(TimeSpan.FromSeconds(seconds));
			Console.WriteLine($"Work done");
			_workerEvent.Set();
			Console.WriteLine($"Waiting work thread");
			_mainEvent.WaitOne();
			Console.WriteLine($"Start second");
			Thread.Sleep(TimeSpan.FromSeconds(seconds));
			Console.WriteLine($"Work done");
			_workerEvent.Set();
		}
		
		
		public override void Run()
		{
			var t = new Thread(()=>Process(10));
			t.Start();
			
			Console.WriteLine($"Waiting another thread work");
			_workerEvent.WaitOne();
			Console.WriteLine($"First comp");
			Console.WriteLine($"main thread");
			Thread.Sleep(TimeSpan.FromSeconds(5));
			_mainEvent.Set();
			Console.WriteLine($"Now running second thread");
			_workerEvent.Set();
			Console.WriteLine($"Second comp");

			t.Join();
		}
	}
}