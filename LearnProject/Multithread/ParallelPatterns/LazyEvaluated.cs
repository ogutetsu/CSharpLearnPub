using System;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.ParallelPatterns
{
	public class LazyEvaluated : ConsoleMenu
	{
		async Task ProcessAsync()
		{
			var unsafeState = new UnsafeState();
			Task[] tasks = new Task[4];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Run(() => Worker(unsafeState));
			}

			await Task.WhenAll(tasks);
			Console.WriteLine("--------------");
			
			var firstState = new DoubleCheckedLocking();
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Run(() => Worker(firstState));
			}

			await Task.WhenAll(tasks);
			Console.WriteLine("--------------");
			
			var secondState = new BCLDoubleChecked();
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Run(() => Worker(secondState));
			}

			await Task.WhenAll(tasks);
			Console.WriteLine("--------------");
			
			var lazy = new Lazy<ValueToAccess>(Compute);
			var thirdState = new LazyWrapper(lazy);
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Run(() => Worker(thirdState));
			}
			await Task.WhenAll(tasks);
			Console.WriteLine("--------------");
			
			var fourthState = new BCLThreadSafeFactory();
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Run(() => Worker(fourthState));
			}
			await Task.WhenAll(tasks);
			Console.WriteLine("--------------");

		}

		void Worker(IHasValue state)
		{
			Console.WriteLine($"Worker runs thread id {Thread.CurrentThread.ManagedThreadId}");
			Console.WriteLine($"State value : {state.Value.Text}");
		}

		static ValueToAccess Compute()
		{
			Console.WriteLine($"The value being constructed on a thread id {Thread.CurrentThread.ManagedThreadId}");
			Thread.Sleep(TimeSpan.FromSeconds(1));
			return new ValueToAccess($"Constructed on thread id {Thread.CurrentThread.ManagedThreadId}");
		}

		interface IHasValue
		{
			ValueToAccess Value { get; }
		}

		class ValueToAccess
		{
			private readonly string _text;

			public ValueToAccess(string text)
			{
				_text = text;
			}

			public string Text => _text;
		}

		class UnsafeState : IHasValue
		{
			private ValueToAccess _value;
			public ValueToAccess Value => _value ?? (_value = Compute());
		}

		class DoubleCheckedLocking : IHasValue
		{
			private readonly object _syncRoot = new object();
			private volatile ValueToAccess _value;

			public ValueToAccess Value
			{
				get
				{
					if (_value == null)
					{
						lock (_syncRoot)
						{
							if (_value == null) _value = Compute();
						}
					}

					return _value;
				}
			}
		}

		class BCLDoubleChecked : IHasValue
		{
			private object _syncRoot = new object();
			private ValueToAccess _value;
			private bool _initialized;

			public ValueToAccess Value => LazyInitializer.EnsureInitialized(
				ref _value, ref _initialized, ref _syncRoot, Compute
			);
		}

		class BCLThreadSafeFactory : IHasValue
		{
			private ValueToAccess _value;
			public ValueToAccess Value => LazyInitializer.EnsureInitialized(ref _value, Compute);
		}

		class LazyWrapper : IHasValue
		{
			private readonly Lazy<ValueToAccess> _value;

			public LazyWrapper(Lazy<ValueToAccess> value)
			{
				_value = value;
			}
			public ValueToAccess Value => _value.Value;
		}
		
		public override void Run()
		{
			var t = ProcessAsync();
			t.GetAwaiter().GetResult();
		}
	}
}