using System;
using System.Diagnostics;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadPriority : ConsoleMenu
	{

		class Sample
		{
			private bool _isStopped = false;

			public void Stop() => _isStopped = true;

			public void Count()
			{
				long cnt = 0;
				while (!_isStopped)
				{
					cnt++;
				}
				Console.WriteLine($"{Thread.CurrentThread.Name} : {Thread.CurrentThread.Priority,11} priority : cnt = {cnt,13:N0}" );
			}
		}
		
		
		
		public override void Run()
		{
			Console.WriteLine($"Priority {Thread.CurrentThread.Priority}");
			RunTread();
			Thread.Sleep(TimeSpan.FromSeconds(2));
			Console.WriteLine($"Single Core");
			Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
			RunTread();
		}

		private void RunTread()
		{
			var sample = new Sample();
			var t1 = new Thread(sample.Count);
			t1.Name = "Thread1";
			var t2 = new Thread(sample.Count);
			t2.Name = "Thread2";

			t1.Priority = System.Threading.ThreadPriority.Highest;
			t2.Priority = System.Threading.ThreadPriority.Lowest;
			t1.Start();
			t2.Start();
			
			Thread.Sleep(TimeSpan.FromSeconds(2));
			sample.Stop();

			t1.Join();
			t2.Join();

		}
		
	}
}