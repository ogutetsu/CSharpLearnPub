using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Version6
{
	public class AwaitParallel : ConsoleMenu
	{
		async System.Threading.Tasks.Task AsyncProcess()
		{
			Task<string> t1 = GetInfoAsync("Task 1", 3);
			Task<string> t2 = GetInfoAsync("Task 2", 5);

			string[] results = await System.Threading.Tasks.Task.WhenAll(t1, t2);
			foreach (var result in results)
			{
				Console.WriteLine(result);
			}
		}

		async Task<string> GetInfoAsync(string name, int seconds)
		{
			await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(seconds));
			return $"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}";
		}
		
		public override void Run()
		{
			System.Threading.Tasks.Task t = AsyncProcess();
			t.Wait();
		}
	}
}