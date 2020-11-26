using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Pool
{
	public class ThreadPoolDelegate : ConsoleMenu
	{

		private delegate string OnPool(out int threadId);

		private void CallBack(IAsyncResult res)
		{
			Console.WriteLine($"Start callback");
			Console.WriteLine($"State {res.AsyncState}");
			Console.WriteLine($"Thread pool : {Thread.CurrentThread.IsThreadPoolThread}");
			Console.WriteLine($"Thread pool worker id : {Thread.CurrentThread.ManagedThreadId}");
		}

		private string Test(out int threadId)
		{
			Console.WriteLine($"Start");
			Console.WriteLine($"Thread pool : {Thread.CurrentThread.IsThreadPoolThread}");
			Thread.Sleep(TimeSpan.FromSeconds(2));
			threadId = Thread.CurrentThread.ManagedThreadId;
			return $"Thread pool worker id : {threadId}";
		}
		
		public override void Run()
		{
			int threadId = 0;
			OnPool poolDelegate = Test;
			
			var t = new Thread(() => Test(out threadId));
			t.Start();
			t.Join();
			
			Console.WriteLine($"Thread id : {threadId}");

			IAsyncResult res = poolDelegate.BeginInvoke(out threadId, CallBack, "delegate async call");
			res.AsyncWaitHandle.WaitOne();

			string resStr = poolDelegate.EndInvoke(out threadId, res);
			
			Console.WriteLine($"Thread pool worker id : {threadId}");
			Console.WriteLine(resStr);
			Thread.Sleep(TimeSpan.FromSeconds(2));

		}

		
	}
}