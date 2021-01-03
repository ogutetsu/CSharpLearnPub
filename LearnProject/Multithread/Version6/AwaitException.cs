using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Version6
{
	public class AwaitException : ConsoleMenu
	{
		async Task AsyncProcess()
		{
			Console.WriteLine($"Single exception");

			try
			{
				string result = await GetInfoAsync("Task 1", 2);
				Console.WriteLine(result);
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e}");
			}
			
			Console.WriteLine();
			Console.WriteLine($"Multiple exception");

			Task<string> t1 = GetInfoAsync("Task 1", 3);
			Task<string> t2 = GetInfoAsync("Task 2", 2);
			try
			{
				string[] results = await System.Threading.Tasks.Task.WhenAll(t1, t2);
				Console.WriteLine(results.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine($"{e}");
			}
			
			Console.WriteLine();
			Console.WriteLine($"Multiple exception AggregateException");

			t1 = GetInfoAsync("Task 1", 3);
			t2 = GetInfoAsync("Task 2", 2);
			Task<string[]> t3 = System.Threading.Tasks.Task.WhenAll(t1, t2);
			try
			{
				string[] results = await t3;
				Console.WriteLine(results.Length);
			}
			catch
			{
				var ae = t3.Exception.Flatten();
				var exceptions = ae.InnerExceptions;
				Console.WriteLine($"{exceptions.Count}");
				foreach (var e in exceptions)
				{
					Console.WriteLine(e);
					Console.WriteLine();
				}
			}
			
			Console.WriteLine();
			Console.WriteLine($"await finally");

			try
			{
				string result = await GetInfoAsync("Task 1", 2);
				Console.WriteLine(result);
			}
			catch (Exception e)
			{
				await Task.Delay(TimeSpan.FromSeconds(1));
				Console.WriteLine($"Catch await : {e}");
			}
			finally
			{
				await Task.Delay(TimeSpan.FromSeconds(2));
				Console.WriteLine($"Finally");
			}


		}
		
		
		async Task<string> GetInfoAsync(string name, int seconds)
		{
			await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(seconds));
			throw new Exception($"{name} !");
		}
		public override void Run()
		{
			System.Threading.Tasks.Task t = AsyncProcess();
			t.Wait();
		}
	}
}