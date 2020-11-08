using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadException : ConsoleMenu
	{
		private void Sample1()
		{
			try
			{
				Console.WriteLine($"Start thread 1...");
				Thread.Sleep(TimeSpan.FromSeconds(1));
				throw new Exception("Thread 1 Exception");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			
		}

		private void Sample2()
		{
			Console.WriteLine($"Start thread 2...");
			Thread.Sleep(TimeSpan.FromSeconds(2));
			throw new Exception("Thread 2 Exception");
		}
		
		public override void Run()
		{
			var t = new Thread(Sample1);
			t.Start();
			t.Join();
			try
			{
				t = new Thread(Sample2);
				t.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}