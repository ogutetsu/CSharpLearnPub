using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadSemaphoreSlim : ConsoleMenu
	{
		static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

		static void AccessDatabase(string name, int seconds)
		{
			Console.WriteLine($"{name} wait");
			_semaphore.Wait();
			Console.WriteLine($"{name} access");
			Thread.Sleep(TimeSpan.FromSeconds(seconds));
			Console.WriteLine($"{name} is completed");
			_semaphore.Release();
		}
		
		public override void Run()
		{
			for (int i = 1; i <= 6; i++)
			{
				string threadName = "Thread " + i;
				int secondsToWait = 2 + 2 * i;
				var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
				t.Start();
			}
			
		}
		
	}
}