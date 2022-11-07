using Elia.Handler;
using System.Net.Sockets;

namespace Elia.Network
{
    internal class Connection : IDisposable
    {
        public Connection(TcpClient client, RequestHandler handler, CancellationToken cancellationToken)
        {
            _client = client;
            _handler = handler;
            _cancellationToken = cancellationToken;
            ReadingLoop = ReadLoop();
        }

        public bool ClientConnected => _client.Connected;

        private readonly RequestHandler _handler;

        private readonly TcpClient _client;
        private readonly CancellationToken _cancellationToken;
        private readonly Task ReadingLoop;

        private const int HeaderLenght = 4;

        private async Task ReadLoop()
        {
            while (true)
            {
                var header = await ReadNetworkStreamAsync(HeaderLenght);
                CheckCancalizationToken();

                int contentLenght = BitConverter.ToInt32(header);

                var content = await ReadNetworkStreamAsync(contentLenght);

                var response = await _handler.Handle(content, _client);

                await _client.GetStream().WriteAsync(response, _cancellationToken);
            }
        }

        private void CheckCancalizationToken()
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                _client.Close();
                throw new TaskCanceledException();
            }
        }

        private async Task<byte[]> ReadNetworkStreamAsync(int count)
        {
            byte[] buffer = new byte[count];

            await _client
                    .GetStream()
                    .ReadAsync(buffer, _cancellationToken);

            return buffer;
        }

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed == false)
            {
                _disposed = true;
                ReadingLoop.Dispose();
                _client.Dispose();
            }
        }
    }
}
