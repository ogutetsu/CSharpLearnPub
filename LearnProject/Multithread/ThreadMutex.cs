using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadMutex : ConsoleMenu
	{
		public override void Run()
		{
			//動作を確認するには、2つの実行ファイルを実行する必要があります。
			string mutexName = "Mutex";
			using (var m = new Mutex(false, mutexName))
			{
				if (!m.WaitOne(TimeSpan.FromSeconds(5), false))
				{
					Console.WriteLine("Running2");
				}
				else
				{
					Console.WriteLine("Running1");
					Console.ReadLine();
					m.ReleaseMutex();
				}
			}
		}
	}
}