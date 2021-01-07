using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using LearnLib;
using Timer = System.Timers.Timer;

namespace LearnProject.Multithread.ReactiveExtensions
{
	public class AsyncRx : ConsoleMenu
	{

		async Task<T> AwaitOnObservable<T>(IObservable<T> observable)
		{
			T obj = await observable;
			Console.WriteLine($"{obj}");
			return obj;
		}

		Task<string> LongRunningOperationTaskAsync(string name)
		{
			return Task.Run(() => LongRunningOperation(name));
		}

		IObservable<string> LongRunningOperationAsync(string name)
		{
			return Observable.Start(() => LongRunningOperation(name));
		}

		string LongRunningOperation(string name)
		{
			Thread.Sleep(TimeSpan.FromSeconds(1));
			return $"Task {name} is completed. Thread id : {Thread.CurrentThread.ManagedThreadId}";
		}

		IDisposable OutputToConsole(IObservable<EventPattern<ElapsedEventArgs>> sequence)
		{
			return sequence.Subscribe(
				obj => Console.WriteLine($"{obj.EventArgs.SignalTime}"),
				ex => Console.WriteLine($"Error : {ex.Message}"),
				() => Console.WriteLine("Completed"));
		}
		IDisposable OutputToConsole<T>(IObservable<T> sequence)
		{
			return sequence.Subscribe(
				obj => Console.WriteLine($"{obj}"),
				ex => Console.WriteLine($"Error : {ex.Message}"),
				() => Console.WriteLine("Completed"));
		}

		delegate string AsyncDelegate(string name);
		
		public override void Run()
		{
			IObservable<string> o = LongRunningOperationAsync("Task1");
			using (var sub = OutputToConsole(o))
			{
				Thread.Sleep(TimeSpan.FromSeconds(2));
			}
			Console.WriteLine("------------------");
			
			Task<string> t = LongRunningOperationTaskAsync("Task1");
			using (var sub = OutputToConsole(t.ToObservable()))
			{
				Thread.Sleep(TimeSpan.FromSeconds(2));
			}
			Console.WriteLine("------------------");

			try
			{
				AsyncDelegate asyncDelegate = LongRunningOperation;
				Func<string, IObservable<string>> observableFactory =
					Observable.FromAsyncPattern<string, string>(asyncDelegate.BeginInvoke, asyncDelegate.EndInvoke);

				o = observableFactory("Task3");
				using (var sub = OutputToConsole(o))
				{
					Thread.Sleep(TimeSpan.FromSeconds(2));
				}

				Console.WriteLine("------------------");

				o = observableFactory("Task4");
				AwaitOnObservable(o).Wait();
				Console.WriteLine("------------------");
			}
			catch (AggregateException e)
			{
				Console.WriteLine(e);
			}

			using (var timer = new Timer(1000))
			{
				var ot = Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(
					h => timer.Elapsed += h,
					h => timer.Elapsed -= h
				);
				timer.Start();

				using (var sub = OutputToConsole(ot))
				{
					Thread.Sleep(TimeSpan.FromSeconds(5));
				}
				Console.WriteLine("-----------------");
				timer.Stop();
			}
			
			
		}
	}
}