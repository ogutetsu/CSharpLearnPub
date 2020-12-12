using System;
using System.Runtime.CompilerServices;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread.Version6
{
	public class AwaitCustomType : ConsoleMenu
	{
		async System.Threading.Tasks.Task AsyncProcess()
		{
			var sync = new CustomAwaitable(true);
			string result = await sync;
			Console.WriteLine(result);
			
			var async = new CustomAwaitable(false);
			result = await async;
			Console.WriteLine(result);
		}

		class CustomAwaitable
		{
			private readonly bool _complete;

			public CustomAwaitable(bool complete)
			{
				_complete = complete;
			}

			public CustomAwaiter GetAwaiter()
			{
				return new CustomAwaiter(_complete);
			}


		}

		class CustomAwaiter : INotifyCompletion
		{
			private string _result = "Complete sync";
			private readonly bool _complete;

			public bool IsCompleted => _complete;

			public CustomAwaiter(bool complete)
			{
				_complete = complete;
			}

			public string GetResult()
			{
				return _result;
			}
			
			
			public void OnCompleted(Action continuation)
			{
				ThreadPool.QueueUserWorkItem(state =>
				{
					Thread.Sleep(TimeSpan.FromSeconds(1));
					_result = GetInfo();
					continuation?.Invoke();
				});
			}

			private string GetInfo()
			{
				return
					$"thread id {Thread.CurrentThread.ManagedThreadId} thread pool {Thread.CurrentThread.IsThreadPoolThread}";
			}
		}
		
		public override void Run()
		{
			System.Threading.Tasks.Task t = AsyncProcess();
			t.Wait();
		}
	}
}