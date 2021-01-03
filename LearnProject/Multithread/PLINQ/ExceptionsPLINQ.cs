using System;
using System.Collections.Generic;
using System.Linq;
using LearnLib;

namespace LearnProject.Multithread.PLINQ
{
	public class ExceptionsPLINQ : ConsoleMenu
	{
		public override void Run()
		{
			IEnumerable<int> numbers = Enumerable.Range(-5, 10);

			var query = from number in numbers select 100/number;

			try
			{
				foreach (var n in query)
				{
					Console.WriteLine(n);
				}
			}
			catch (DivideByZeroException)
			{
				Console.WriteLine("Divided by zero");
			}
			Console.WriteLine("-------------");
			Console.WriteLine("Sequential LINQ");
			Console.WriteLine();

			var parallelQuery = from number in numbers.AsParallel() select 100 / number;

			try
			{
				parallelQuery.ForAll(Console.WriteLine);
			}
			catch (DivideByZeroException)
			{
				Console.WriteLine("Divided by zero");
			}
			catch (AggregateException e)
			{
				e.Flatten().Handle(ex =>
				{
					if (ex is DivideByZeroException)
					{
						Console.WriteLine("Divided by zero - aggregate exception");
						return true;
					}

					return false;
				});
			}
			Console.WriteLine("Finished");


		}
	}
}