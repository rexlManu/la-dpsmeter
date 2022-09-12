using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace LostArkLogger.State.Socket;

public class StateSocketHandler : WebSocketBehavior
{
    private Timer _Timer;
    int _interval = 10;

    protected override void OnOpen()
    {
        Console.WriteLine("[State] Client connected");
        Start();
    }

    private void Start()
    {
        _Timer = new Timer(Tick, null, _interval, Timeout.Infinite);
    }

    protected override void OnClose(CloseEventArgs e)
    {
        Console.WriteLine("[State] Client disconnected");

        // Unregister from state updates
        LostArkLogger.Instance.StateManager.RemoveHandler(this);
        try
        {
            _Timer.Dispose();
        }
        catch (Exception exception)
        {
        }
    }

    protected override void OnMessage(MessageEventArgs e)
    {
    }

    private void Tick(object? state)
    {
        try
        {
            var game = LostArkLogger.Instance.StateManager.GetState();
            // format to json with lowercase property names
            var json = JsonSerializer.Serialize(game, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Send(json);
        }
        catch (Exception e)
        {
            Start();
        }
        finally
        {
            _Timer?.Change(_interval, Timeout.Infinite);
        }
    }
}