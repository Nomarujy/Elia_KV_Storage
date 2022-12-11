using System.Net.Sockets;

namespace Elia.NetCore.Entity
{
    internal class Client : IDisposable
    {
        public delegate void OnDisconectedHandler(Client connection);
        public event OnDisconectedHandler? OnDisconected;

        private readonly TcpClient _client;
        private readonly Task? ReadLoop;

        public Client(TcpClient client, Func<TcpClient, Task> loop)
        {
            _client = client;
            ReadLoop = loop.Invoke(_client)
                .ContinueWith(_ => OnDisconected?.Invoke(this));
        }
        ~Client()
        {
            Dispose();
        }

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed == false)
            {
                _disposed = true;
                ReadLoop?.Dispose();
                _client.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
