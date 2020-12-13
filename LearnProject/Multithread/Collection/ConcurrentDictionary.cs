using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using LearnLib;

namespace LearnProject.Multithread.Collection
{
	public class ConcurrentDictionary : ConsoleMenu
	{
		private const string Item = "Item";
		private const int Iterations = 10000000;
		public string CurrentItem;
		
		public override void Run()
		{
			var concurrentDictionary = new ConcurrentDictionary<int, string>();
			var dictionary = new Dictionary<int, string>();
			
			var sw = new Stopwatch();
			
			sw.Start();

			for (int i = 0; i < Iterations; i++)
			{
				lock (dictionary)
				{
					dictionary[i] = Item;
				}
			}

			sw.Stop();
			Console.WriteLine($"write lock : {sw.Elapsed}");
			
			sw.Restart();
			for (int i = 0; i < Iterations; i++)
			{
				concurrentDictionary[i] = Item;
				
			}
			sw.Stop();
			Console.WriteLine($"write concurrentDic : {sw.Elapsed}");
			
			sw.Restart();
			for (int i = 0; i < Iterations; i++)
			{
				lock (dictionary)
				{
					CurrentItem = dictionary[i];
				}
			}

			sw.Stop();
			Console.WriteLine($"read lock : {sw.Elapsed}");
			
			sw.Restart();
			for (int i = 0; i < Iterations; i++)
			{
				CurrentItem = concurrentDictionary[i];
			}

			sw.Stop();
			Console.WriteLine($"read concurrentDic : {sw.Elapsed}");


		}
	}
}