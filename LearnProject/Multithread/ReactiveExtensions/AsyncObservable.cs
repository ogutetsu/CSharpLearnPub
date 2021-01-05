using System;
using System.Collections.Generic;
using LearnLib;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;


namespace LearnProject.Multithread.ReactiveExtensions
{
	public class AsyncObservable : ConsoleMenu
	{

		IEnumerable<int> EnumerableEventSeq()
		{
			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(TimeSpan.FromSeconds(0.5));
				yield return i;
			}
		}
		public override void Run()
		{
			foreach (var i in EnumerableEventSeq())
			{
				Console.Write(i);
			}
			
			Console.WriteLine();
			Console.WriteLine("IEnumerable");

			IObservable<int> o = EnumerableEventSeq().ToObservable();
			using (IDisposable subscription = o.Subscribe(Console.Write))
			{
				Console.WriteLine();
				Console.WriteLine("IObservable");
			}

			o = EnumerableEventSeq().ToObservable().SubscribeOn(TaskPoolScheduler.Default);
			using (IDisposable subscription = o.Subscribe(Console.Write))
			{
				Console.WriteLine();
				Console.WriteLine("IObservable async");
				Console.ReadLine();
			}

		}
	}
}