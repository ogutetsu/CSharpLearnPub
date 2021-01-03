using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.PLINQ
{
	public class DataPartitioningPLINQ : ConsoleMenu
	{
		void PrintInfo(string typeName)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(150));
			Console.WriteLine($"{typeName} thread id {Thread.CurrentThread.ManagedThreadId}");
		}
		
		string EmulateProcess(string typeName)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(150));
			Console.WriteLine($"{typeName} thread id {Thread.CurrentThread.ManagedThreadId}" +
			                  $"{(typeName.Length % 2 == 0? "even" : "odd")} length.");
			return typeName;
		}
		
		IEnumerable<string> GetTypes()
		{
			var types = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(a => a.GetExportedTypes());

			return from type in types
				where type.Name.StartsWith("Task")
				select type.Name;
		}

		class StringPartitioner : Partitioner<string>
		{
			private readonly IEnumerable<string> _data;

			public StringPartitioner(IEnumerable<string> data)
			{
				_data = data;
			}

			public override bool SupportsDynamicPartitions => false;

			public override IList<IEnumerator<string>> GetPartitions(int partitionCount)
			{
				var result = new List<IEnumerator<string>>(partitionCount);
				for (int i = 0; i < partitionCount; i++)
				{
					result.Add(CreateEnumerator(i, partitionCount));
				}

				return result;
			}

			IEnumerator<string> CreateEnumerator(int partitionNumber, int partitionCount)
			{
				int evenPartitions = partitionCount / 2;
				bool isEven = partitionNumber % 2 == 0;
				int step = isEven ? evenPartitions : partitionCount - evenPartitions;

				int startIndex = partitionNumber / 2 + partitionNumber % 2;

				var q = _data.Where(v => !(v.Length % 2 == 0 ^ isEven)
				                         || partitionCount == 1).Skip(startIndex - 1);
				return q.Where((x, i) => i % step == 0).GetEnumerator();
			}
			
		}
		
		
		public override void Run()
		{
			var timer = Stopwatch.StartNew();
			var partitioner = new StringPartitioner(GetTypes());
			var parallelQuery = from t in partitioner.AsParallel() select EmulateProcess(t);
			
			parallelQuery.ForAll(PrintInfo);
			int count = parallelQuery.Count();
			timer.Stop();
			Console.WriteLine("------------");
			Console.WriteLine($"Total items : {count}");
			Console.WriteLine($"Time elapsed : {timer.Elapsed}");
		}
		
	}
}