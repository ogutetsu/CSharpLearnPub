using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
	public class ThreadPoolAsync : ConsoleMenu
	{

		private void Async(object state)
		{
			Console.WriteLine($"state : {state ?? "null"}");
			Console.WriteLine($"Worker thread id : {Thread.CurrentThread.ManagedThreadId}");
			Thread.Sleep(TimeSpan.FromSeconds(2));
		}
		
		public override void Run()
		{
			const int x = 1;
			const int y = 2;
			const string state = "state 2";

			ThreadPool.QueueUserWorkItem(Async);
			Thread.Sleep(TimeSpan.FromSeconds(1));

			ThreadPool.QueueUserWorkItem(Async, "async state");
			Thread.Sleep(TimeSpan.FromSeconds(1));

			ThreadPool.QueueUserWorkItem(state =>
			{
				Console.WriteLine($"state : {state}");
				Console.WriteLine($"Worker thread id : {Thread.CurrentThread.ManagedThreadId}");
				Thread.Sleep(TimeSpan.FromSeconds(2));
			}, "lambda state");

			ThreadPool.QueueUserWorkItem(_ =>
			{
				Console.WriteLine($"state : {x + y}, {state}");
				Console.WriteLine($"Worker thread id : {Thread.CurrentThread.ManagedThreadId}");
				Thread.Sleep(TimeSpan.FromSeconds(2));
			}, "lambda state");

			Thread.Sleep(TimeSpan.FromSeconds(2));
		}
	}
}