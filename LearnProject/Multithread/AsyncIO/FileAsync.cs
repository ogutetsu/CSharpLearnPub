using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Text.Encoding;
using LearnLib;

namespace LearnProject.Multithread.AsyncIO
{
	public class FileAsync : ConsoleMenu
	{
		private int BUFFER_SIZE = 4096;

		async Task ProcessAsyncIO()
		{
			using (var stream = new FileStream("test1.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None,
				BUFFER_SIZE))
			{
				Console.WriteLine($"1. Use I/O Thread : {stream.IsAsync}");
				byte[] buffer = UTF8.GetBytes(CreateFileContent());
				var writeTask =
					Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
				await writeTask;
			}

			using (var stream = new FileStream("test2.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None,
				BUFFER_SIZE, FileOptions.Asynchronous))
			{
				Console.WriteLine($"2. Uses I/O Thread : {stream.IsAsync}");
				byte[] buffer = UTF8.GetBytes(CreateFileContent());
				var writeTask =
					Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);
				await writeTask;
			}

			using (var stream = File.Create("test3.txt", BUFFER_SIZE, FileOptions.Asynchronous))
			using(var sw = new StreamWriter(stream))
			{
				Console.WriteLine($"3. Uses I/O Thread : {stream.IsAsync}");
				await sw.WriteAsync(CreateFileContent());
			}
			using(var sw = new StreamWriter("test4.txt", true))
			{
				Console.WriteLine($"4. Uses I/O Thread : {((FileStream)sw.BaseStream).IsAsync}");
				await sw.WriteAsync(CreateFileContent());
			}
			Console.WriteLine("Starting parsing files in parallel");

			var readTasks = new Task<long>[4];
			for (int i = 0; i < readTasks.Length; i++)
			{
				string fileName = $"test{i + 1}.txt";
				readTasks[i] = SumFileContent(fileName);
			}

			long[] sums = await Task.WhenAll(readTasks);
			
			Console.WriteLine($"Sum in all files : {sums.Sum()}");
			
			Console.WriteLine($"Deleting files");
			
			Task[] deleteTasks = new Task[4];
			for (int i = 0; i < deleteTasks.Length; i++)
			{
				string fileName = $"test{i + 1}.txt";
				deleteTasks[i] = SimulateAsuncDelete(fileName);
			}

			await Task.WhenAll(deleteTasks);
			Console.WriteLine("Deleting complete.");
		}

		private Task SimulateAsuncDelete(string fileName)
		{
			return Task.Run(() => File.Delete(fileName));
		}

		string CreateFileContent()
		{
			var sb = new StringBuilder();
			for (int i = 0; i < 100000; i++)
			{
				sb.Append($"{new Random(i).Next(0, 99999)}");
				sb.AppendLine();
			}

			return sb.ToString();
		}

		async Task<long> SumFileContent(string fileName)
		{
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None, BUFFER_SIZE,
				FileOptions.Asynchronous))
			{
				using (var sr = new StreamReader(stream))
				{
					long sum = 0;
					while (sr.Peek() > -1)
					{
						string line = await sr.ReadLineAsync();
						sum += long.Parse(line);
					}

					return sum;
				}
			}
		}
		
		
		
		public override void Run()
		{
			var t = ProcessAsyncIO();
			t.GetAwaiter().GetResult();
		}
	}
}