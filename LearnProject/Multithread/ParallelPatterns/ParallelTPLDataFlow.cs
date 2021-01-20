using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using LearnLib;

namespace LearnProject.Multithread.ParallelPatterns
{
	public class ParallelTPLDataFlow : ConsoleMenu
	{
		async Task ProcessAsync()
		{
			var cts = new CancellationTokenSource();
			Random rnd = new Random(DateTime.Now.Millisecond);

			Task.Run(() =>
			{
				if(Console.ReadKey().KeyChar == 'c') cts.Cancel();
			}, cts.Token);
			
			var inputBlock = new BufferBlock<int>(
				new DataflowBlockOptions(){ BoundedCapacity = 5, CancellationToken = cts.Token} );
			
			var convertToDecimalBlock = new TransformBlock<int, decimal>(
				n =>
				{
					decimal result = Convert.ToDecimal(n * 100);
					Console.WriteLine($"Decimal Converter sent {result} to the next stage on thread id {Thread.CurrentThread.ManagedThreadId}");
					Thread.Sleep(TimeSpan.FromMilliseconds(rnd.Next(200)));
					return result;
				},
				new ExecutionDataflowBlockOptions() {MaxDegreeOfParallelism = 4, CancellationToken = cts.Token});
			
			var stringifyBlock = new TransformBlock<decimal, string>(
				n =>
				{
					string result = $"--{n.ToString("C", CultureInfo.GetCultureInfo("en-us"))}--";
					Console.WriteLine($"String Formatter sent {result} to the next stage on thread id {Thread.CurrentThread.ManagedThreadId}");
					Thread.Sleep(TimeSpan.FromMilliseconds(rnd.Next(200)));
					return result;
				},
				new ExecutionDataflowBlockOptions() {MaxDegreeOfParallelism = 4, CancellationToken = cts.Token});
			
			var outputBlock = new ActionBlock<string>(
				s =>
				{
					Console.WriteLine($"The final result is {s} on thread id {Thread.CurrentThread.ManagedThreadId}");
				},
				new ExecutionDataflowBlockOptions() {MaxDegreeOfParallelism = 4, CancellationToken = cts.Token});

			inputBlock.LinkTo(convertToDecimalBlock, new DataflowLinkOptions() {PropagateCompletion = true});
			convertToDecimalBlock.LinkTo(stringifyBlock, new DataflowLinkOptions() {PropagateCompletion = true});
			stringifyBlock.LinkTo(outputBlock, new DataflowLinkOptions() {PropagateCompletion = true});

			try
			{
				Parallel.For(0, 20, new ParallelOptions() {MaxDegreeOfParallelism = 4, CancellationToken = cts.Token}
					, i =>
					{
						Console.WriteLine(
							$"added {i} to source data on thread id {Thread.CurrentThread.ManagedThreadId}");
						inputBlock.SendAsync(i).GetAwaiter().GetResult();
					});
				inputBlock.Complete();
				await outputBlock.Completion;
				Console.WriteLine("Press ENTER to exit.");
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("Operation has been canceled press ENTER to exit");
			}

			Console.ReadLine();


		}
		
		public override void Run()
		{
			var t = ProcessAsync();
			t.GetAwaiter().GetResult();
		}
	}
}