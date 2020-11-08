using System;
using System.Threading;
using LearnLib;

namespace LearnProject.Multithread
{
	public class ThreadAtomic : ConsoleMenu
	{
		
		abstract class Base
		{
			public abstract void Inc();
			public abstract void Dec();
		}
        
		class Sample : Base
		{
			public int cnt { get; private set; }
			public override void Inc() => cnt++;
			public override void Dec() => cnt--;
		}

		class SampleNoLock : Base
		{
			public readonly object _sync = new object();
			public int cnt;
			public override void Inc()
			{
				Interlocked.Increment(ref cnt);
			}

			public override void Dec()
			{
				Interlocked.Decrement(ref cnt);
			}
		}
		
		private void Count(Base b)
		{
			for (int i = 0; i < 10000; i++)
			{
				b.Inc();
				b.Dec();
			}
		}
		
		public override void Run()
		{
			
			var s = new Sample();
			var t1 = new Thread(() => Count(s));
			var t2 = new Thread(() => Count(s));
			var t3 = new Thread(() => Count(s));
			t1.Start();
			t2.Start();
			t3.Start();
			t1.Join();
			t2.Join();
			t3.Join();
			
			Console.WriteLine($"Total : {s.cnt}");
			
			var s1 = new SampleNoLock();
			t1 = new Thread(() => Count(s1));
			t2 = new Thread(() => Count(s1));
			t3 = new Thread(() => Count(s1));
			t1.Start();
			t2.Start();
			t3.Start();
			t1.Join();
			t2.Join();
			t3.Join();
			
			Console.WriteLine($"Total : {s1.cnt}");

		}
	}
}