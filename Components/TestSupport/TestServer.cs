using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace TestSupport
{
    public class TestServer<T> where T : class
    {
        private readonly int _wait;
        private readonly Thread _thread;
        private IWebHost _server;

        public TestServer(string url, int wait)
        {
            _wait = wait;
            _thread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                var builder = new WebHostBuilder()
                    .UseKestrel()
                    .UseStartup<T>()
                    .UseUrls(url);
                _server = builder.Build();
                _server.Run();
            });
        }

        public void Start()
        {
            _thread.Start();

            Thread.Sleep(_wait);
        }

        public void Stop()
        {
            _server.Dispose();
            _thread.Join();
        }
    }
}