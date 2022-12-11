using Elia.NetCore.Entity;
using System.Net.Sockets;

namespace Elia.NetCore
{
    public class NetworkCore : IDisposable
    {
        private readonly TcpListener _tcpListener;

        private readonly List<Client> _conections = new();

        public NetworkCore() : this(new NetworkConfiguration()) { }

        public NetworkCore(NetworkConfiguration cfg)
        {
            _tcpListener = new TcpListener(cfg.IP, cfg.Port);
        }

        public async Task StartAsync(Func<TcpClient, Task> clientHandler)
        {
            _tcpListener.Start();

            while (true)
            {
                var client = await _tcpListener.AcceptTcpClientAsync();

                var connection = new Client(client, clientHandler);
                connection.OnDisconected += CloseConnection;

                _conections.Add(connection);
            }
        }

        private void CloseConnection(Client connection)
        {
            _conections.Remove(connection);

            connection.Dispose();
            connection.OnDisconected -= CloseConnection;
        }


        private readonly bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _tcpListener.Stop();

                foreach (var item in _conections)
                {
                    item.OnDisconected -= CloseConnection;
                    item.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}
