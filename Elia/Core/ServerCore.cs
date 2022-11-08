﻿using Elia.Handler;
using Elia.Network.ReadingLoop;
using System.Net;
using System.Net.Sockets;

namespace Elia.Network
{
    public class ServerCore
    {
        private readonly TcpListener _tcpListener;
        private readonly ReadingLoopProvider _loopProvider;

        private readonly SynchronizedCollection<Connection> _conections = new();

        public ServerCore(int port, ReadingLoopProvider loopProvider)
        {
            _tcpListener = new TcpListener(IPAddress.Loopback, port);
            _loopProvider = loopProvider;
        }

        public async Task StartAsync()
        {
            _tcpListener.Start();

            while (true)
            {
                var client = await _tcpListener.AcceptTcpClientAsync();

                var connection = new Connection(client, _loopProvider);
                connection.OnDisconected += CloseConnection;

                _conections.Add(connection);
            }
        }

        private void CloseConnection(Connection connection)
        {
            Console.WriteLine("Removed");
            _conections.Remove(connection);

            connection.Dispose();
            connection.OnDisconected -= CloseConnection;
        }
    }
}
