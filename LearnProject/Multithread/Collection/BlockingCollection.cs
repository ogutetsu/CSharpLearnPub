using System;
using System.Collections.Concurrent;
using LearnLib;

namespace LearnProject.Multithread.Collection
{
	public class BlockingCollection : ConsoleMenu
	{

		async System.Threading.Tasks.Task RunProg(IProducerConsumerCollection<CustomTask> collection = null)
		{
			var taskCollection = new BlockingCollection<CustomTask>();
			if (collection != null)
			{
				taskCollection = new BlockingCollection<CustomTask>(collection);
			}

			var taskSource = System.Threading.Tasks.Task.Run(() => TaskProducer(taskCollection));
			
			System.Threading.Tasks.Task[] processors = new System.Threading.Tasks.Task[4];
			for (int i = 0; i < processors.Length; i++)
			{
				string processorId = $"Processor {i}";
				processors[i] = System.Threading.Tasks.Task.Run(() => TaskProcessor(taskCollection, processorId));
			}

			await taskSource;
			await System.Threading.Tasks.Task.WhenAll(processors);

		}

		async System.Threading.Tasks.Task TaskProducer(BlockingCollection<CustomTask> collection)
		{
			for (int i = 0; i < 20; i++)
			{
				await System.Threading.Tasks.Task.Delay(20);
				var workItem = new CustomTask() { Id = i};
				collection.Add(workItem);
				Console.WriteLine($"Task {workItem.Id}");
			}
			collection.CompleteAdding();
		}

		async System.Threading.Tasks.Task TaskProcessor(BlockingCollection<CustomTask> collection, string name)
		{
			await GetRandomDelay();
			foreach (var item in collection.GetConsumingEnumerable())
			{
				Console.WriteLine($"Task {item.Id} processed {name}");
				await GetRandomDelay();
			}
		}

		System.Threading.Tasks.Task GetRandomDelay()
		{
			int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
			return System.Threading.Tasks.Task.Delay(delay);
		}

		class CustomTask
		{
			public int Id { get; set; }
		}
		
		
		public override void Run()
		{
			Console.WriteLine($"Queue BlockingCollection");
			Console.WriteLine();
			System.Threading.Tasks.Task t = RunProg();
			t.Wait();
			
			Console.WriteLine();
			Console.WriteLine($"Stack BlockingCollection");
			Console.WriteLine();
			t = RunProg(new ConcurrentStack<CustomTask>());
			t.Wait();

		}
	}
}