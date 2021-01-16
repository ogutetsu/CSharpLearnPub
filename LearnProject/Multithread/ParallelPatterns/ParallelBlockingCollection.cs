using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.ParallelPatterns
{
	public class ParallelBlockingCollection : ConsoleMenu
	{
		private const int CollectionsNumber = 4;
		private const int Count = 5;

		void CreateInitialValues(BlockingCollection<int>[] sourceArrays, CancellationTokenSource cts)
		{
			Parallel.For(0, sourceArrays.Length * Count, (j, state) =>
			{
				if (cts.Token.IsCancellationRequested)
				{
					state.Stop();
				}

				int number = GetRandomNumver(j);
				int k = BlockingCollection<int>.TryAddToAny(sourceArrays, j);
				if (k >= 0)
				{
					Console.WriteLine($"added {j} to source data on thread id {Thread.CurrentThread.ManagedThreadId}");
					Thread.Sleep(TimeSpan.FromMilliseconds(number));
				}
			});
			foreach (var source in sourceArrays)
			{
				source.CompleteAdding();
			}
		}

		int GetRandomNumver(int seed)
		{
			return new Random(seed).Next(500);
		}

		class PipelineWorker<TInput, TOutput>
		{
			private Func<TInput, TOutput> _processor;
			private Action<TInput> _outputProcessor;
			private BlockingCollection<TInput>[] _intput;
			private CancellationToken _token;
			private Random _rnd;
			
			public BlockingCollection<TOutput>[] Output { get; private set; }
			public string Name { get; private set; }

			public PipelineWorker(
				BlockingCollection<TInput>[] input,
				Func<TInput, TOutput> processor,
				CancellationToken token,
				string name
			)
			{
				_intput = input;
				Output = new BlockingCollection<TOutput>[_intput.Length];
				for (int i = 0; i < Output.Length; i++)
				{
					Output[i] = null == input[i] ? null : new BlockingCollection<TOutput>(Count);
				}

				_processor = processor;
				_token = token;
				Name = name;
				_rnd = new Random(DateTime.Now.Millisecond);
			}

			public PipelineWorker(
				BlockingCollection<TInput>[] input,
				Action<TInput> processor,
				CancellationToken token,
				string name
			)
			{
				_intput = input;
				_outputProcessor = processor;
				_token = token;
				Name = name;
				Output = null;
				_rnd = new Random(DateTime.Now.Millisecond);

			}

			public void Run()
			{
				Console.WriteLine($"{Name} is run");
				while (!_intput.All(bc => bc.IsCompleted) &&
				       !_token.IsCancellationRequested)
				{
					TInput receivedItem;
					int i = BlockingCollection<TInput>.TryTakeFromAny(
						_intput, out receivedItem, 50, _token);
					if (i >= 0)
					{
						if (Output != null)
						{
							TOutput outputItem = _processor(receivedItem);
							BlockingCollection<TOutput>.AddToAny(Output, outputItem);
							Console.WriteLine($"{Name} sent {outputItem} to next, on thread id {Thread.CurrentThread.ManagedThreadId}");
							Thread.Sleep(TimeSpan.FromMilliseconds(_rnd.Next(200)));
						}
						else
						{
							_outputProcessor(receivedItem);
						}
					}
					else
					{
						Thread.Sleep(TimeSpan.FromMilliseconds(50));
					}
				}

				if (Output != null)
				{
					foreach (var bc in Output)
					{
						bc.CompleteAdding();
					}
				}
			}
		}
		
		public override void Run()
		{
			var cts = new CancellationTokenSource();
			Task.Run(() =>
			{
				if (Console.ReadKey().KeyChar == 'c') cts.Cancel();
			}, cts.Token);
			
			var sourceArrays = new BlockingCollection<int>[CollectionsNumber];
			for (int i = 0; i < sourceArrays.Length; i++)
			{
				sourceArrays[i] = new BlockingCollection<int>(Count);
			}
			
			var convertToDecimal = new PipelineWorker<int, decimal>(
				sourceArrays,
				n => Convert.ToDecimal(n*100),
				cts.Token,
				"Decimal Converter");
			
			var stringifyNumber = new PipelineWorker<decimal, string>(
				convertToDecimal.Output,
				s => $"--{s.ToString("C", CultureInfo.GetCultureInfo("en-us"))}--",
				cts.Token,
				"String Formatter"
				);
			
			var outputResultToConsole = new PipelineWorker<string, string>(
				stringifyNumber.Output,
				s => Console.WriteLine($"final result {s} thread id {Thread.CurrentThread.ManagedThreadId}"),
				cts.Token,
				"Console Output"
				);

			try
			{
				Parallel.Invoke(
					() => CreateInitialValues(sourceArrays, cts),
					() => convertToDecimal.Run(),
					() => stringifyNumber.Run(),
					() => outputResultToConsole.Run());
			}
			catch (AggregateException e)
			{
				foreach (var ex in e.InnerExceptions)
				{
					Console.WriteLine(ex.Message + ex.StackTrace);
				}
			}

			if (cts.Token.IsCancellationRequested)
			{
				Console.WriteLine("Operation has been canceled! Press ENTER to exit.");
			}
			else
			{
				Console.WriteLine("Press ENTER to exit.");
			}

			Console.ReadLine();

		}
	}
}