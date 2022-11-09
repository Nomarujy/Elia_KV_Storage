using Elia.Core;
using System.Net.Sockets;

namespace Elia.Network
{
    internal class Connection : IDisposable
    {
        private readonly TcpClient _client;
        private readonly Task? ReadLoop;
        private bool _disposed;

        public Connection(TcpClient client, ReadingLoopProvider _readingLoopProvider)
        {
            _client = client;
            ReadLoop = _readingLoopProvider.CreateLoopAsync(_client);
            ReadLoop.ContinueWith(Task => ReadLoopExit());
        }

        public delegate void OnDisconectedHandler(Connection connection);
        public event OnDisconectedHandler? OnDisconected;

        private void ReadLoopExit()
        {
            OnDisconected?.Invoke(this);
        }

        public void Dispose()
        {
            if (_disposed == false)
            {
                _disposed = true;
                ReadLoop?.Dispose();
                _client.Dispose();
            }
        }
    }
}
