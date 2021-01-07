using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.ReactiveExtensions
{
	public class ObservableObject : ConsoleMenu
	{
		IDisposable OutpuToConsole<T>(IObservable<T> sequence)
		{
			return sequence.Subscribe(obj => Console.WriteLine($"{obj}"),
				ex => Console.WriteLine($"Error: {ex.Message}"),
				() => Console.WriteLine("Completed"));
		}
		
		public override void Run()
		{
			IObservable<int> o = Observable.Return(0);
			using (var sub = OutpuToConsole(o)) ;
			Console.WriteLine(" --------------- ");

			o = Observable.Empty<int>();
			using (var sub = OutpuToConsole(o)) ;
			Console.WriteLine(" --------------- ");

			o = Observable.Throw<int>(new Exception());
			using (var sub = OutpuToConsole(o)) ;
			Console.WriteLine(" --------------- ");

			o = Observable.Repeat(42);
			using (var sub = OutpuToConsole(o.Take(5))) ;
			Console.WriteLine(" --------------- ");
			
			o = Observable.Range(0,10);
			using (var sub = OutpuToConsole(o)) ;
			Console.WriteLine(" --------------- ");

			o = Observable.Create<int>(ob =>
			{
				for (int i = 0; i < 10; i++)
				{
					ob.OnNext(i);
				}
				return Disposable.Empty;
			});
			using (var sub = OutpuToConsole(o)) ;
			Console.WriteLine(" --------------- ");

			o = Observable.Generate(
				0,
				i => i < 5,
				i => ++i,
				i => i * 2);
			using (var sub = OutpuToConsole(o)) ;
			Console.WriteLine(" --------------- ");

			IObservable<long> ol = Observable.Interval(TimeSpan.FromSeconds(1));
			using (var sub = OutpuToConsole(ol))
			{
				Thread.Sleep(TimeSpan.FromSeconds(3));
			}
			Console.WriteLine(" --------------- ");

			ol = Observable.Timer(DateTimeOffset.Now.AddSeconds(2));
			using (var sub = OutpuToConsole(ol))
			{
				Thread.Sleep(TimeSpan.FromSeconds(3));
			}
			Console.WriteLine(" --------------- ");


		}
	}
}