using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Version6
{
	public class AwaitLambda : ConsoleMenu
	{
		async System.Threading.Tasks.Task AsyncProcess()
		{
			Func<string, Task<string>> lambda = async name =>
			{
				await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2));
				return
					$"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}";
			};
			string result = await lambda("async lambda");
			Console.WriteLine(result);
		}
		public override void Run()
		{
			System.Threading.Tasks.Task t = AsyncProcess();
			t.Wait();
		}
	}
}