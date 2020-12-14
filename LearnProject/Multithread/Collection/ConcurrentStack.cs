using System;
using System.Collections.Concurrent;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Collection
{
	public class ConcurrentStack : ConsoleMenu
	{
		class CustomTask
		{
			public int Id { get; set; }
		}
		async System.Threading.Tasks.Task RunProgram()
		{
			var taskStack = new ConcurrentStack<CustomTask>();
			var cts = new CancellationTokenSource();

			var taskSource = System.Threading.Tasks.Task.Run(() => TaskProducer(taskStack));
			
			System.Threading.Tasks.Task[] processors = new System.Threading.Tasks.Task[4];
			for (int i = 0; i < 4; i++)
			{
				string processorId = (i + 1).ToString();
				processors[i] = System.Threading.Tasks.Task.Run(() => TaskProcessor(taskStack, $"Processor {processorId}", cts.Token));
			}

			await taskSource;
			cts.CancelAfter(TimeSpan.FromSeconds(2));

			await System.Threading.Tasks.Task.WhenAll(processors);
		}

		async System.Threading.Tasks.Task TaskProducer(ConcurrentStack<CustomTask> stack)
		{
			for(int i = 0; i < 20; i++)
			{
				await System.Threading.Tasks.Task.Delay(50);
				var workItem = new CustomTask() {Id = i + 1};
				stack.Push(workItem);
				Console.WriteLine($"Task {workItem.Id}");
			}
		}

		async System.Threading.Tasks.Task TaskProcessor(ConcurrentStack<CustomTask> stack, string name,
			CancellationToken token)
		{
			await GetRandomDelay();
			do
			{
				CustomTask workItem;
				bool popSuccess = stack.TryPop(out workItem);
				if (popSuccess)
				{
					Console.WriteLine($"Task {workItem.Id} process {name}");
				}

				await GetRandomDelay();

			} while (!token.IsCancellationRequested);
			
		}
		
		System.Threading.Tasks.Task GetRandomDelay()
		{
			int delay = new Random(DateTime.Now.Millisecond).Next(1,500);
			return System.Threading.Tasks.Task.Delay(delay);
		}

		public override void Run()
		{
			System.Threading.Tasks.Task t = RunProgram();
			t.Wait();
		}
	}
}