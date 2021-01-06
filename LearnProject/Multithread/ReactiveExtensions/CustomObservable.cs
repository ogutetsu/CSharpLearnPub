using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.ReactiveExtensions
{
	public class CustomObservable : ConsoleMenu
	{
		class CustomObserver : IObserver<int>
		{
			public void OnCompleted()
			{
				Console.WriteLine("Completed");
			}

			public void OnError(Exception error)
			{
				Console.WriteLine($"Error : {error.Message}");
			}

			public void OnNext(int value)
			{
				Console.WriteLine($"Next value : {value}; Thread id : {Thread.CurrentThread.ManagedThreadId}");
			}
		}

		class CustomSequence : IObservable<int>
		{
			private readonly IEnumerable<int> _numbers;

			public CustomSequence(IEnumerable<int> numbers)
			{
				_numbers = numbers;
			}
			
			public IDisposable Subscribe(IObserver<int> observer)
			{
				foreach (var number in _numbers)
				{
					observer.OnNext(number);
				}
				observer.OnCompleted();
				return Disposable.Empty;;
			}
		}
		
		public override void Run()
		{
			var observer = new CustomObserver();
			var goodObservable = new CustomSequence(new []{1,2,3,4,5});
			var badObservable = new CustomSequence(null);
			
			using(IDisposable subscription = goodObservable.Subscribe(observer))
			{}

			using (IDisposable subscription = goodObservable.SubscribeOn(TaskPoolScheduler.Default).Subscribe(observer))
			{
				Thread.Sleep(TimeSpan.FromMilliseconds(100));
				Console.WriteLine("Press ENTER");
				Console.ReadLine();
			}
			using (IDisposable subscription = badObservable.SubscribeOn(TaskPoolScheduler.Default).Subscribe(observer))
			{
				Thread.Sleep(TimeSpan.FromMilliseconds(100));
				Console.WriteLine("Press ENTER");
				Console.ReadLine();
			}

			
			
		}
	}
}