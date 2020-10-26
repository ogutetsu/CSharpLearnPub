using System;
using LearnLib;
using System.Threading;

namespace LearnProject.Multithread
{
	public class ThreadState : ConsoleMenu
	{
		public override void Run()
		{
			Thread t = new Thread(PrintState);
			Thread t2 = new Thread(Nothing);
			
			Console.WriteLine(t.ThreadState.ToString());
			t.Start();
			t2.Start();
			//WaitSleepJoinにならない場合は、カウント数を増やしてみる
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine($"waitsleepjoin : {t.ThreadState.ToString()}");
			}
			
			Thread.Sleep(TimeSpan.FromSeconds(4));
			t.Interrupt();
			
			t2.Join();
			
			Console.WriteLine($"t : {t.ThreadState.ToString()}");
			Console.WriteLine($"t2 : {t2.ThreadState.ToString()}");

		}

		void Nothing()
		{
			Thread.Sleep(TimeSpan.FromSeconds(1));
		}

		void PrintState()
		{
			Console.WriteLine(Thread.CurrentThread.ThreadState.ToString());
			for (int i = 0; i < 5; i++)
			{
				Thread.Sleep(TimeSpan.FromSeconds(2));
				Console.WriteLine(i);
			}
		}
		
	}
}