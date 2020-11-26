using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
	public class ThreadPoolCancel : ConsoleMenu
	{
		void Async1(CancellationToken token)
		{
			Console.WriteLine($"Start first task");
			for (int i = 0; i < 5; i++)
			{
				if (token.IsCancellationRequested)
				{
					Console.WriteLine($"First task canceled");
					return;
				}
				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
			Console.WriteLine($"First task comp");
		}

		void Async2(CancellationToken token)
		{
			try
			{
				Console.WriteLine($"Start second task");
				for (int i = 0; i < 5; i++)
				{
					token.ThrowIfCancellationRequested();
					Thread.Sleep(TimeSpan.FromSeconds(1));
				}
				Console.WriteLine($"Second task comp");

			}
			catch (OperationCanceledException e)
			{
				Console.WriteLine($"Second task canceled");
			}
		}

		void Async3(CancellationToken token)
		{
			bool cancelFlag = false;
			token.Register(() => cancelFlag = true);
			Console.WriteLine($"Start third task");
			for (int i = 0; i < 5; i++)
			{
				if (cancelFlag)
				{
					Console.WriteLine($"Third task canceled");
					return;
				}
				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
			Console.WriteLine($"Third task comp");
		}
		
		public override void Run()
		{
			using (var cts = new CancellationTokenSource())
			{
				CancellationToken token = cts.Token;
				ThreadPool.QueueUserWorkItem(_ => Async1(token));
				Thread.Sleep(TimeSpan.FromSeconds(2));
				cts.Cancel();
			}

			using (var cts = new CancellationTokenSource())
			{
				CancellationToken token = cts.Token;
				ThreadPool.QueueUserWorkItem(_ => Async2(token));
				Thread.Sleep(TimeSpan.FromSeconds(2));
				cts.Cancel();
			}
			
			using (var cts = new CancellationTokenSource())
			{
				CancellationToken token = cts.Token;
				ThreadPool.QueueUserWorkItem(_ => Async3(token));
				Thread.Sleep(TimeSpan.FromSeconds(2));
				cts.Cancel();
			}
			
			Thread.Sleep(TimeSpan.FromSeconds(3));
		}
	}
}