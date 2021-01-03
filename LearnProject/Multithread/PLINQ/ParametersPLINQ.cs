using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.PLINQ
{
	public class ParametersPLINQ : ConsoleMenu
	{
		string EmulateProcessing(string typeName)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(new Random(DateTime.Now.Millisecond).Next(250,350)));
			Console.WriteLine($"{typeName} thread id {Thread.CurrentThread.ManagedThreadId}");
			return typeName;
		}

		IEnumerable<string> GetTypes()
		{
			return from assembly in AppDomain.CurrentDomain.GetAssemblies() 
				from type in assembly.GetExportedTypes() 
				where type.Name.StartsWith("Task")
				orderby type.Name.Length
				select type.Name;
		}
		
		public override void Run()
		{
			var parallelQuery = from t in GetTypes().AsParallel() select EmulateProcessing(t);
			var cts = new CancellationTokenSource();
			cts.CancelAfter(TimeSpan.FromSeconds(3));

			try
			{
				parallelQuery
					.WithDegreeOfParallelism(Environment.ProcessorCount)
					.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
					.WithMergeOptions(ParallelMergeOptions.Default)
					.WithCancellation(cts.Token)
					.ForAll(Console.WriteLine);
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("------------");
				Console.WriteLine("Operation canceled");
			}
			
			Console.WriteLine("------------");
			Console.WriteLine("Unordered PLINQ");
			var unorderedQuery = from i in ParallelEnumerable.Range(1, 30) select i;

			foreach (var i in unorderedQuery)
			{
				Console.WriteLine(i);
			}
			
			Console.WriteLine("------------");
			Console.WriteLine("Ordered PLINQ");
			var orderedQuery = from i in ParallelEnumerable.Range(1, 30).AsOrdered() select i;

			foreach (var i in orderedQuery)
			{
				Console.WriteLine(i);
			}

		}
	}
}