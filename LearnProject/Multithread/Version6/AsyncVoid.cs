using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Version6
{
	public class AsyncVoid : ConsoleMenu
	{

		async Task AsyncTaskWithErrors()
		{
			string result = await GetInfoAsync("TaskException", 2);
			Console.WriteLine(result);
		}

		async void AsyncVoidWithErrors()
		{
			string result = await GetInfoAsync("AsyncVoidException", 2);
			Console.WriteLine(result);
		}

		async Task AsyncTask()
		{
			string result = await GetInfoAsync("Task", 2);
			Console.WriteLine(result);
		}

		async void AsyncVoidFunc()
		{
			string result = await GetInfoAsync("AsyncVoid", 2);
			Console.WriteLine(result);
		}

		async Task<string> GetInfoAsync(string name, int seconds)
		{
			await Task.Delay(TimeSpan.FromSeconds(seconds));
			if (name.Contains("Exception"))
			{
				throw new Exception($"{name} !");
			}

			return $"Task {name} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}";
		}
		public override void Run()
		{
			Task t = AsyncTask();
			t.Wait();
			
			AsyncVoidFunc();
			Thread.Sleep(TimeSpan.FromSeconds(3));

			t = AsyncTaskWithErrors();
			while (!t.IsFaulted)
			{
				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
			Console.WriteLine(t.Exception);
			
			/*try
			{
				AsyncVoidWithErrors();
				Thread.Sleep(TimeSpan.FromSeconds(3));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}*/
			

			int[] numbers = {1, 2, 3, 4, 5};
			Array.ForEach(numbers, async number =>
			{
				await Task.Delay(TimeSpan.FromSeconds(1));
				if(number == 3) throw new Exception($"Exception !!!");
				Console.WriteLine(number);
			});

			Console.ReadLine();

		}
	}
}