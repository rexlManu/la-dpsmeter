using System.Collections.Concurrent;
using System.Net.Http.Headers;
using LostArkLogger.Utilities;

namespace LostArkLogger
{
    public class HttpBridge
    {
        // Docker host IP
        private static string Host = $"http://{LostArkLogger.Instance.ConfigurationProvider.Configuration.WebHost}";
        private static int Port = LostArkLogger.Instance.ConfigurationProvider.Configuration.WebPort;

        private static Parser parser;

        private readonly HttpClient http = new HttpClient();
        private readonly ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
        private Thread thread;

        public string[] args;

        public HttpBridge()
        {
            Oodle.Init();
        }

        public void Start()
        {
            parser = new Parser();
            Logger.onLogAppend += (string log) => { EnqueueMessage(log); };

            this.thread = new Thread(this.Run);
            this.thread.Start();

            Console.WriteLine($"All connections are ready. Sending data to {Host}:{Port}");
            EnqueueMessage(255, "Connected", Host, Port.ToString());
        }

        private void EnqueueMessage(string log)
        {
            this.messageQueue.Enqueue(log);
        }

        private void EnqueueMessage(int id, params string[] elements)
        {
            var log = id + "|" + DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") +
                      "|" + String.Join("|", elements);
            this.messageQueue.Enqueue(log);
        }

        private Task<HttpResponseMessage> SendRequest(string sendMessage)
        {
#if DEBUG
            Console.WriteLine("Sending: " + sendMessage);
#endif
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Host + ":" + Port);
            request.Content = new StringContent(sendMessage);
            var mediaTypeHeaderValue = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            mediaTypeHeaderValue.CharSet = "utf-8";
            request.Content.Headers.ContentType = mediaTypeHeaderValue;

            return this.http.SendAsync(request);
        }

        private async void Run()
        {
            while (true)
            {
                if (this.messageQueue.TryDequeue(out var sendMessage))
                {
                    try
                    {
                        await this.SendRequest(sendMessage);
                    }
                    catch (Exception e)
                    {
#if DEBUG
                        Console.WriteLine(e.Message);
#endif
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        public async void Stop()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Host + ":" + Port);
                var log = 255 + "|" +
                          DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") + "|" +
                          "Exiting";
                await this.SendRequest(log);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine("Error: " + e.Message);
#endif
            }
        }
    }
}