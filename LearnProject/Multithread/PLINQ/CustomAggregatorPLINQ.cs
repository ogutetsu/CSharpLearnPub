using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.PLINQ
{
	public class CustomAggregatorPLINQ : ConsoleMenu
	{
		ConcurrentDictionary<char, int> Info(ConcurrentDictionary<char, int> taskTotal, string item)
		{
			foreach (var c in item)
			{
				if (taskTotal.ContainsKey(c))
				{
					taskTotal[c] = taskTotal[c] + 1;
				}
				else
				{
					taskTotal[c] = 1;
				}
			}
			Console.WriteLine($"{item} thread id {Thread.CurrentThread.ManagedThreadId}");
			return taskTotal;
		}

		ConcurrentDictionary<char, int> Marge(ConcurrentDictionary<char, int> total,
			ConcurrentDictionary<char, int> taskTotal)
		{
			foreach (var key in taskTotal.Keys)
			{
				if (total.ContainsKey(key))
				{
					total[key] = total[key] + taskTotal[key];
				}
				else
				{
					total[key] = taskTotal[key];
				}
			}
			Console.WriteLine("---------");
			Console.WriteLine($"Total aggregate thread id {Thread.CurrentThread.ManagedThreadId}");
			return total;
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
		
		public override void Run()
		{
			var parallelQuery = from t in GetTypes().AsParallel() select t;

			var parallelAggregator = parallelQuery.Aggregate(
				() => new ConcurrentDictionary<char, int>(),
				(taskTotal, item) => Info(taskTotal, item),
				(total, taskTotal) => Marge(total, taskTotal),
				total => total);
			
			Console.WriteLine();
			Console.WriteLine("type name : ");
			var orderedKeys = from k in parallelAggregator.Keys
				orderby parallelAggregator[k] descending
				select k;

			foreach (var c in orderedKeys)
			{
				Console.WriteLine($"Letter '{c}' --- {parallelAggregator[c]} times ");
			}

		}
	}
}