using Elia.Handler.Context;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

public static class Program
{
    static void Main(string[] args)
    {
        var client = new TcpClient();
        client.Connect(IPAddress.Loopback, 8989);

        var json = PostJson();

        SendRequest(client, json);

        Thread.Sleep(5000);
        Console.WriteLine("SLEEp");

        json = GetJson();

        SendRequest(client, json);

        while (true)
        {
            Console.ReadLine();
        }
    }

    private static void SendRequest(TcpClient client, string json)
    {
        byte[] data = Encoding.UTF8.GetBytes(json);

        byte[] lenght = BitConverter.GetBytes(data.Length);

        client.GetStream()
            .WriteAsync(lenght);

        client.GetStream()
            .WriteAsync(data);
    }

    private static string GetJson()
    {
        Request request = new()
        {
            ValuePath = new()
            {
                Application = "App",
                Topic = "Topic",
                Key = "Key",
            },
        };

        return JsonSerializer.Serialize(request);
    }

    private static string PostJson()
    {
        Request request = new()
        {
            ValuePath = new()
            {
                Application = "App",
                Topic = "Topic",
                Key = "Key",
            },
            ValueEntity = new()
            {
                Value = "SomeValue",
                Timestamp = DateTime.Now,
            },
        };

        return JsonSerializer.Serialize(request);
    }
}