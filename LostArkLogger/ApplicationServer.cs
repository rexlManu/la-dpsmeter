using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace LostArkLogger.Utilities;

public class ApplicationServer
{
    private Parser parser;
    private readonly ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();
    private Thread thread;
    private HttpServer _Server;
    // private Fleck.WebSocketServer server = new Fleck.WebSocketServer("ws://0.0.0.0:1338");
    // private List<IWebSocketConnection> Connections = new List<IWebSocketConnection>();

    public ApplicationServer()
    {
        Oodle.Init();
    }

    public void Start()
    {
        parser = new Parser();
        
        
        this._Server = new HttpServer(LostArkLogger.Instance.ConfigurationProvider.Configuration.WebPort);
        this._Server.DocumentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/frontend";
        
        this._Server.OnGet += (sender, e) => {
            var req = e.Request;
            var res = e.Response;

            var path = req.RawUrl;

            if (path == "/")
                path += "index.html";

            byte[] contents;

            if (!e.TryReadFile (path, out contents)) {
                res.StatusCode = (int) HttpStatusCode.NotFound;

                return;
            }

            if (path.EndsWith (".html")) {
                res.ContentType = "text/html";
                res.ContentEncoding = Encoding.UTF8;
            }
            else if (path.EndsWith (".js")) {
                res.ContentType = "application/javascript";
                res.ContentEncoding = Encoding.UTF8;
            }

            res.ContentLength64 = contents.LongLength;

            res.Close (contents, true);
        };


        _Server.AddWebSocketService<SocketHandler>("/");
        Logger.onLogAppend += (string log) => { EnqueueMessage(log); };

        this.thread = new Thread(this.Run);
        this.thread.Start();

        _Server.Start();

        if (_Server.IsListening) {
            Console.WriteLine ("Listening on port {0}, and providing WebSocket services:", _Server.Port);

            foreach (var path in _Server.WebSocketServices.Paths)
                Console.WriteLine ("- {0}", path);
        }
        
        // server.Start(connection =>
        // {
        //     connection.OnOpen = () => { Connections.Add(connection); };
        //     connection.OnClose = () => { Connections.Remove(connection); };
        //     connection.OnMessage = message =>
        //     {
        //         if (!message.Contains(":")) return;
        //         var parts = message.Split(":");
        //         var channelName = parts[0];
        //         if (channelName == "logs")
        //         {
        //             // list all file names from directory Logger.logsPath
        //             var files = System.IO.Directory.GetFiles(Logger.logsPath);
        //             var fileNames = new List<string>();
        //             foreach (var file in files)
        //             {
        //                 fileNames.Add(System.IO.Path.GetFileName(file));
        //             }
        //
        //             var answer = JsonConvert.SerializeObject(fileNames);
        //             // base64 encode answer
        //             var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(answer));
        //             connection.Send("logs:" + base64);
        //             return;
        //         }
        //
        //         if (channelName == "download")
        //         {
        //             // base64 decode message
        //             var fileName = Convert.FromBase64String(parts[1]);
        //             var filePath = System.IO.Path.Combine(Logger.logsPath, Encoding.UTF8.GetString(fileName));
        //             if (!System.IO.File.Exists(filePath)) return;
        //             // go though all lines in file and send them to client
        //             var lines = System.IO.File.ReadAllLines(filePath);
        //             foreach (var line in lines)
        //             {
        //                 // base64 encode line
        //                 var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(line));
        //                 connection.Send("log:" + base64);
        //             }
        //             // send a uwu as last message to indicate end of file
        //             connection.Send("log:uwu");
        //             return;
        //         }
        //         // var data = Convert.FromBase64String(parts[1]);
        //     };
        // });
    }

    private void Publish(string message)
    {
        // encode message as base64
        var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(message));
        var outgoingMessage = $"packet:{data}";

        foreach (var id in this._Server.WebSocketServices["/"].Sessions.ActiveIDs)
        {
            this._Server.WebSocketServices["/"].Sessions.SendTo(outgoingMessage, id);
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
        _Server.Stop();
    }
}

public class SocketHandler : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Console.WriteLine("Message received: " + e.Data);
    }
}