﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LearnLib;

namespace LearnProject.Multithread.AsyncIO
{
	public class AsyncHTTP : ConsoleMenu
	{
		async Task GetResponseAsync(string url)
		{
			using (var client = new HttpClient())
			{
				HttpResponseMessage responseMessage = await client.GetAsync(url);
				string responseHeaders = responseMessage.Headers.ToString();
				string response = await responseMessage.Content.ReadAsStringAsync();
				
				Console.WriteLine("Response headers:");
				Console.WriteLine(responseHeaders);
				Console.WriteLine("Response body:");
				Console.WriteLine(response);
			}
			
		}

		class AsyncHttpServer
		{
			private readonly HttpListener _listener;

			private const string RESPONSE_TEMPLATE =
				"<html><head><title>Test</title></head><body><h2>Testpage</h2>" +
				"<h4>Today is : {0}</h4></body></html>";

			public AsyncHttpServer(int portNumber)
			{
				_listener = new HttpListener();
				_listener.Prefixes.Add($"http://localhost:{portNumber}/");
			}

			public async Task Start()
			{
				_listener.Start();
				while (true)
				{
					var ctx = await _listener.GetContextAsync();
					Console.WriteLine("Client connected...");
					var response = string.Format(RESPONSE_TEMPLATE, DateTime.Now);

					using (var sw = new StreamWriter(ctx.Response.OutputStream))
					{
						await sw.WriteAsync(response);
						await sw.FlushAsync();
					}
				}
			}

			public async Task Stop()
			{
				_listener.Abort();
			}
			
		}
		
		
		public override void Run()
		{
			var server = new AsyncHttpServer(1234);
			var t = Task.Run(() => server.Start());
			Console.WriteLine("Listening on port 1234. http://localhost:1234 in your browser.");
			Console.WriteLine("Trying to connect:");
			Console.WriteLine();
			
			GetResponseAsync("http://localhost:1234").GetAwaiter().GetResult();
			
			Console.WriteLine();
			Console.WriteLine("Press Enter to stop the server.");
			Console.ReadLine();
			
			server.Stop().GetAwaiter().GetResult();
		}
	}
}