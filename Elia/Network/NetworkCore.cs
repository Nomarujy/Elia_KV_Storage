using System.Net;
using System.Net.Sockets;
using Elia.Handler;
using System.Collections.Generic;

namespace Elia.Network
{
    internal class NetworkCore
    {
        private readonly TcpListener _tcpListener;
        private readonly RequestHandler _handler;

        private readonly SynchronizedCollection<Connection> _readers = new();
        private readonly Timer _timer;

        public NetworkCore(int port, RequestHandler requestHandler)
        {
            _tcpListener = new TcpListener(IPAddress.Loopback, port);
            _handler = requestHandler;
            _timer = new(new TimerCallback(RemoveDisconected), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        public async void Start()
        {
            Console.WriteLine("Start listening");
            _tcpListener.Start();
            CancellationTokenSource src = new();

            while (true)
            {
                var client = await _tcpListener.AcceptTcpClientAsync();

                _readers.Add(new Connection(client, _handler, src.Token));
            }
        }

        public void RemoveDisconected(object? state)
        {
            Console.WriteLine("Collecting clients");
            for (int i = _readers.Count - 1; i > 0; i--)
            {
                if (_readers[i].ClientConnected == false)
                {
                    Console.WriteLine($"Collected {i}");
                    _readers[i].Dispose();
                    _readers.RemoveAt(i);
                }
            }
        }
    }
}
