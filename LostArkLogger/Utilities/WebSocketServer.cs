using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Text;
using Fleck;
using Newtonsoft.Json;

namespace LostArkLogger.Utilities;

public class WebSocketServer
{
    private Parser parser;
    private readonly ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
    private Thread thread;
    private Fleck.WebSocketServer server = new Fleck.WebSocketServer("ws://0.0.0.0:1338");
    private List<IWebSocketConnection> Connections = new List<IWebSocketConnection>();

    public WebSocketServer()
    {
        Oodle.Init();
    }

    public void Start()
    {
        parser = new Parser();

        Logger.onLogAppend += (string log) => { EnqueueMessage(log); };

        this.thread = new Thread(this.Run);
        this.thread.Start();

        server.Start(connection =>
        {
            connection.OnOpen = () => { Connections.Add(connection); };
            connection.OnClose = () => { Connections.Remove(connection); };
            connection.OnMessage = message =>
            {
                if (!message.Contains(":")) return;
                var parts = message.Split(":");
                var channelName = parts[0];
                if (channelName == "logs")
                {
                    // list all file names from directory Logger.logsPath
                    var files = System.IO.Directory.GetFiles(Logger.logsPath);
                    var fileNames = new List<string>();
                    foreach (var file in files)
                    {
                        fileNames.Add(System.IO.Path.GetFileName(file));
                    }

                    var answer = JsonConvert.SerializeObject(fileNames);
                    // base64 encode answer
                    var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(answer));
                    connection.Send("logs:" + base64);
                    return;
                }

                if (channelName == "download")
                {
                    // base64 decode message
                    var fileName = Convert.FromBase64String(parts[1]);
                    var filePath = System.IO.Path.Combine(Logger.logsPath, Encoding.UTF8.GetString(fileName));
                    if (!System.IO.File.Exists(filePath)) return;
                    // go though all lines in file and send them to client
                    var lines = System.IO.File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        // base64 encode line
                        var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(line));
                        connection.Send("log:" + base64);
                    }
                    // send a uwu as last message to indicate end of file
                    connection.Send("log:uwu");
                    return;
                }
                // var data = Convert.FromBase64String(parts[1]);
            };
        });
    }

    private void Publish(string message)
    {
        // encode message as base64
        var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        var outgoingMessage = $"packet:{data}";

        foreach (var webSocketConnection in this.Connections)
        {
            try
            {
                webSocketConnection.Send(outgoingMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    private void EnqueueMessage(string log)
    {
        this.messageQueue.Enqueue(log);
    }

    private void EnqueueMessage(int id, params string[] elements)
    {
        var log = id + "|" + DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'") + "|" +
                  String.Join("|", elements);
        this.messageQueue.Enqueue(log);
    }

    private async void Run()
    {
        while (true)
        {
            if (this.messageQueue.TryDequeue(out var sendMessage))
            {
                Publish(sendMessage);
            }
            else
            {
                Thread.Sleep(1);
            }
        }
    }

    public void close()
    {
        server.Dispose();
    }
}