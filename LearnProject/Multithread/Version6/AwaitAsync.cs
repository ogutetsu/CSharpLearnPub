using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Version6
{
	public class AwaitAsync : ConsoleMenu
	{
		System.Threading.Tasks.Task AsyncWithTPL()
		{
			var containerTask = new System.Threading.Tasks.Task(() =>
				{
					Task<string> t = GetInfoAsync("TPL 1");
					t.ContinueWith(task =>
					{
						Console.WriteLine(t.Result);
						Task<string> t2 = GetInfoAsync("TPL 2");
						t2.ContinueWith(innerTask => Console.WriteLine(innerTask.Result),
							TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.AttachedToParent
						);
						t2.ContinueWith(innerTask => Console.WriteLine(innerTask.Exception.InnerException),
							TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);
					},
						TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.AttachedToParent);

					t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException),
						TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);
				}
				);
			containerTask.Start();
			return containerTask;
		}

		async System.Threading.Tasks.Task AsyncWithAwait()
		{
			try
			{
				string result = await GetInfoAsync("Async 1");
				Console.WriteLine(result);
				result = await GetInfoAsync("Async 2");
				Console.WriteLine(result);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}


		async Task<string> GetInfoAsync(string name)
		{
			Console.WriteLine($"Task {name} start");
			await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(2));
			if(name == "TPL 2") throw new Exception("Boom");
			return $"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}";
		}
		
		public override void Run()
		{
			System.Threading.Tasks.Task t = AsyncWithTPL();
			t.Wait();
			t = AsyncWithAwait();
			t.Wait();
		}
	}
}