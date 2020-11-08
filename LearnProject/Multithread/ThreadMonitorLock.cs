using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadMonitorLock : ConsoleMenu
	{
		private void LockSample(object lock1, object lock2)
		{
			lock (lock1)
			{
				Thread.Sleep(1000);
				lock (lock2)
				{
				}
			}
		}
		
		public override void Run()
		{
			object lock1 = new object();
			object lock2 = new object();
			
			new Thread(() => LockSample(lock1, lock2)).Start();

			lock (lock2)
			{
				Thread.Sleep(1000);
				Console.WriteLine($"Monitor.TryEnter");
				if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(3)))
				{
					Console.WriteLine($"Success");
				}
				else
				{
					Console.WriteLine($"Timeout");
				}
			}
			
			new Thread(()=>LockSample(lock1, lock2)).Start();

			Console.WriteLine($"---------------------------");
			lock (lock2)
			{
				Console.WriteLine($"Deadlock");
				Thread.Sleep(1000);
				lock (lock1)
				{
					Console.WriteLine($"Success");
				}
			}
			
		}
	}
}