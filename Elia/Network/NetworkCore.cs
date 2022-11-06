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

        private readonly SynchronizedCollection<MessageReader> _readers = new();

        public NetworkCore(int port, RequestHandler requestHandler)
        {
            _tcpListener = new TcpListener(IPAddress.Loopback, port);
            _handler = requestHandler;
        }

        public void Start()
        {
            Console.WriteLine("Start listening");
            _tcpListener.Start();
            BeginAccept();
        }

        public void Stop()
        {
            _tcpListener.Stop();
        }

        private void BeginAccept()
        {
            _tcpListener.BeginAcceptTcpClient(new(EndAccept), null);
        }

        private void EndAccept(IAsyncResult res)
        {
            var client = _tcpListener.EndAcceptTcpClient(res);
            Console.WriteLine($"New connection {client.GetHashCode()}");
            _readers.Add(new MessageReader(client, _handler));
            BeginAccept();
        }
    }
}
