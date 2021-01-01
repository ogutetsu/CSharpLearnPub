using LearnLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnProject.Multithread.Collection
{
	public class ConcurrentBag : ConsoleMenu
	{
		Dictionary<string, string[]> _contentEmulation = new Dictionary<string, string[]>();

		async System.Threading.Tasks.Task RunProg()
		{
			var bag = new ConcurrentBag<CrawlingTask>();
			string[] urls = {"http://abc", "http://efg", "http://hij", "http://klm"};
			var crawlers = new System.Threading.Tasks.Task[4];
			for (int i = 0; i < 4; i++)
			{
				string crawlerName = $"Crawler {i}";
				bag.Add(new CrawlingTask() {UrlToCrawl = urls[i], ProducerName = "root"});
				crawlers[i] = System.Threading.Tasks.Task.Run(() => Crawl(bag, crawlerName));
			}

			await System.Threading.Tasks.Task.WhenAll(crawlers);
		}

		async System.Threading.Tasks.Task Crawl(ConcurrentBag<CrawlingTask> bag, string crawlerName)
		{
			CrawlingTask task;
			while (bag.TryTake(out task))
			{
				IEnumerable<string> urls = await GetLinksFromContent(task);
				if (urls != null)
				{
					foreach (var url in urls)
					{
						var t = new CrawlingTask()
						{
							UrlToCrawl = url,
							ProducerName = crawlerName
						};
						bag.Add(t);
					}
				}
				Console.WriteLine($"Indexing url {task.UrlToCrawl} posted by {task.ProducerName} is comp {crawlerName}");
			}
		}

		async Task<IEnumerable<string>> GetLinksFromContent(CrawlingTask task)
		{
			await GetRandomDelay();
			if (_contentEmulation.ContainsKey(task.UrlToCrawl)) return _contentEmulation[task.UrlToCrawl];
			return null;
		}

		void CreateLinks()
		{
			_contentEmulation["http://abc/"] = new[] {"http://abc/a.html", "http://abc/b.html"};
			_contentEmulation["http://abc/a.html"] = new[] {"http://abc/c.html", "http://abc/d.html"};
			_contentEmulation["http://abc/b.html"] = new[] {"http://abc/e.html"};

			
			_contentEmulation["http://efg"] = new[] {"http://efg/a.html", "http://efg/b.html"};
			_contentEmulation["http://efg/a.html"] = new[] {"http://efg/c.html", "http://efg/d.html"};
			_contentEmulation["http://efg/b.html"] = new[] {"http://efg/e.html"};
			_contentEmulation["http://efg/c.html"] = new[] {"http://efg/f.html","http://efg/g.html"};

			_contentEmulation["http://hij/"] = new[] {"http://hij/a.html", "http://hij/b.html"};
			_contentEmulation["http://hij/a.html"] = new[] {"http://hij/c.html", "http://hij/d.html"};
			_contentEmulation["http://hij/b.html"] = new[] {"http://hij/e.html"};

			_contentEmulation["http://klm"] = new[] {"http://klm/a.html", "http://klm/b.html"};
			_contentEmulation["http://klm/a.html"] = new[] {"http://klm/c.html", "http://klm/d.html"};
			_contentEmulation["http://klm/b.html"] = new[] {"http://klm/e.html"};
			_contentEmulation["http://klm/c.html"] = new[] {"http://klm/f.html","http://klm/g.html"};

		}

		System.Threading.Tasks.Task GetRandomDelay()
		{
			int delay = new Random(DateTime.Now.Millisecond).Next(150, 200);
			return System.Threading.Tasks.Task.Delay(delay);
		}
		
		public override void Run()
		{
			CreateLinks();
			System.Threading.Tasks.Task t = RunProg();
			t.Wait();
		}
	}

	internal class CrawlingTask
	{
		public string UrlToCrawl { get; set; }
		public string ProducerName { get; set; }
	}
}