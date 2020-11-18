using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadBarrier : ConsoleMenu
	{
		
		static Barrier _barrier = new Barrier(2,
			b => Console.WriteLine($"end {b.CurrentPhaseNumber + 1}"));

		static void Sample(string name, string message, int seconds)
		{
			for (int i = 1; i < 3; i++)
			{
				Console.WriteLine("--------------");
				Thread.Sleep(TimeSpan.FromSeconds(seconds));
				Console.WriteLine($"{name} start {message}");
				Thread.Sleep(TimeSpan.FromSeconds(seconds));
				Console.WriteLine($"{name} finish {message}");
				_barrier.SignalAndWait();
			}
		}
		
		public override void Run()
		{
			var t1 = new Thread(() => Sample("Thread1", "OK", 3));
			var t2 = new Thread(()=> Sample("Thread2", "OK", 1));
			t1.Start();
			t2.Start();

			t1.Join();
			t2.Join();
		}
	}
}