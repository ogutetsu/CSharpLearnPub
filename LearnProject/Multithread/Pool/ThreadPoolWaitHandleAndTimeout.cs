using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
	public class ThreadPoolWaitHandleAndTimeout : ConsoleMenu
	{

		void RunOp(TimeSpan timeout)
		{
			using (var evt = new ManualResetEvent(false))
			using (var cts = new CancellationTokenSource())
			{
				Console.WriteLine($"Regist timout");
				var worker = ThreadPool.RegisterWaitForSingleObject(
					evt,
					(state, isTimeOut) => WorkerOpWait(cts, isTimeOut),
					null,
					timeout,
					true);
				Console.WriteLine($"Start RunOp");
				ThreadPool.QueueUserWorkItem(_ => WorkerOp(cts.Token, evt));
				Thread.Sleep(timeout.Add(TimeSpan.FromSeconds(2)));
				worker.Unregister(evt);
			}
		}

		void WorkerOp(CancellationToken token, ManualResetEvent evt)
		{
			for (int i = 0; i < 6; i++)
			{
				if (token.IsCancellationRequested) return;
				Thread.Sleep(TimeSpan.FromSeconds(1));
			}

			evt.Set();
		}

		void WorkerOpWait(CancellationTokenSource cts, bool isTimeout)
		{
			if (isTimeout)
			{
				cts.Cancel();
				Console.WriteLine($"Timeout cancel");
			}
			else
			{
				Console.WriteLine($"Success");
			}
		}
		
		public override void Run()
		{
			RunOp(TimeSpan.FromSeconds(5));
			RunOp(TimeSpan.FromSeconds(7));
		}
	}
}