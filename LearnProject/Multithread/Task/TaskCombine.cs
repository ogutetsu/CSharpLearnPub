using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.Task
{
	public class TaskCombine : ConsoleMenu
	{
		int TaskMethod(string name, int seconds)
		{
			Console.WriteLine(
				$"Task {name} thread id : {Thread.CurrentThread.ManagedThreadId} pool thread: {Thread.CurrentThread.IsThreadPoolThread}");
			Thread.Sleep(TimeSpan.FromSeconds(seconds));
			return seconds;
		}
		public override void Run()
		{
			var task1 = new Task<int>(()=> TaskMethod("Task 1", 3));
			var task2 = new Task<int>(()=> TaskMethod("Task 2", 2));
			task1.ContinueWith(
				t => Console.WriteLine($"Task 1 {t.Result} Thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}"));
			task1.Start();
			task2.Start();
			
			Thread.Sleep(TimeSpan.FromSeconds(4));

			System.Threading.Tasks.Task continuation = task2.ContinueWith(
				t => Console.WriteLine($"Task 2 {t.Result} thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}"),
				TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously
				);
			
			continuation.GetAwaiter().OnCompleted(() =>
			{
				Console.WriteLine($"Continuation Task Comp Thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}");
			});
			
			Thread.Sleep(TimeSpan.FromSeconds(2));
			Console.WriteLine();
			
			task1 = new Task<int>(() =>
			{
				var innerTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
				
					TaskMethod("Task 2", 5), TaskCreationOptions.AttachedToParent
				);
				return TaskMethod("Task 1", 2);
			});
			
			task1.Start();

			while (!task1.IsCompleted)
			{
				Console.WriteLine(task1.Status);
				Thread.Sleep(TimeSpan.FromSeconds(0.5));
			}
			
			Console.WriteLine(task1.Status);
			Thread.Sleep(TimeSpan.FromSeconds(10));
		}
	}
}