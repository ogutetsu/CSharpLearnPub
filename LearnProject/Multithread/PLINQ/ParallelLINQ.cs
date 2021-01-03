using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.PLINQ
{
	public class ParallelLINQ : ConsoleMenu
	{

		void PrintInfo(string typeName)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(150));
			Console.WriteLine($"{typeName} thread id {Thread.CurrentThread.ManagedThreadId}");
		}

		string EmulateProcess(string typeName)
		{
			Thread.Sleep(TimeSpan.FromMilliseconds(150));
			Console.WriteLine($"{typeName} thread id {Thread.CurrentThread.ManagedThreadId}");
			return typeName;
		}

		IEnumerable<string> GetTypes()
		{
			return from assembly in AppDomain.CurrentDomain.GetAssemblies() 
				from type in assembly.GetExportedTypes() 
				where type.Name.StartsWith("Task")
				select type.Name;
		}
		
		
		public override void Run()
		{
			var sw = new Stopwatch();
			sw.Start();
			var query = from t in GetTypes() select EmulateProcess(t);

			foreach (var typeName in query)
			{
				PrintInfo(typeName);
			}
			
			sw.Stop();
			Console.WriteLine("-------------");
			Console.WriteLine("Sequential LINQ");
			Console.WriteLine($"Time elapsed : {sw.Elapsed}");
			Console.WriteLine("Press ENTER...");
			Console.ReadLine();
			sw.Reset();
			
			sw.Start();
			var parallelQuery = from t in GetTypes().AsParallel() select EmulateProcess(t);
			foreach (var typeName in parallelQuery)
			{
				PrintInfo(typeName);
			}

			sw.Stop();
			Console.WriteLine("-------------");
			Console.WriteLine("Parallel LINQ single thread");
			Console.WriteLine($"Time elapsed : {sw.Elapsed}");
			Console.WriteLine("Press ENTER...");
			Console.ReadLine();
			sw.Reset();
			
			sw.Start();
			parallelQuery = from t in GetTypes().AsParallel() select EmulateProcess(t);
			parallelQuery.ForAll(PrintInfo);
			sw.Stop();
			Console.WriteLine("-------------");
			Console.WriteLine("Parallel LINQ parallel process");
			Console.WriteLine($"Time elapsed : {sw.Elapsed}");
			Console.WriteLine("Press ENTER...");
			Console.ReadLine();
			sw.Reset();
			
			sw.Start();
			query = from t in GetTypes().AsParallel().AsSequential() select EmulateProcess(t);
			foreach (var typeName in query)
			{
				PrintInfo(typeName);
			}
			sw.Stop();
			Console.WriteLine("-------------");
			Console.WriteLine("Parallel LINQ transformed sequential.");
			Console.WriteLine($"Time elapsed : {sw.Elapsed}");
			Console.WriteLine("Press ENTER...");
			Console.ReadLine();

		}
	}
}