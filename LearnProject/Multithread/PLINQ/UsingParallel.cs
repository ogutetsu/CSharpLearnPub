using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.PLINQ
{
	public class UsingParallel : ConsoleMenu
	{

		string EmulateProcess(string taskName)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(new Random(DateTime.Now.Millisecond).Next(250, 350)));
			Console.WriteLine($"{taskName} thread id {Thread.CurrentThread.ManagedThreadId}");
			return taskName;
		}
		
		public override void Run()
		{
			Parallel.Invoke(
				() => EmulateProcess("Task1"),
				() => EmulateProcess("Task2"),
				() => EmulateProcess("Task3")
				);
			var cts = new CancellationTokenSource();

			var result = Parallel.ForEach(
				Enumerable.Range(1,30),
				new ParallelOptions()
				{
					CancellationToken = cts.Token,
					MaxDegreeOfParallelism = Environment.ProcessorCount,
					TaskScheduler = TaskScheduler.Default
				},
				(i, state) =>
				{
					Console.WriteLine(i);
					if (i == 20)
					{
						state.Break();
						Console.WriteLine($"Loop is stopped : {state.IsStopped}");
					}
				}
				);
			Console.WriteLine("------------");
			Console.WriteLine($"Completed : {result.IsCompleted}");
			Console.WriteLine($"Lowest break iteration : {result.LowestBreakIteration}");
		}
	}
}