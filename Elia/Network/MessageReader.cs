using Elia.Handler;
using Elia.Handler.Context;
using System.Net.Sockets;

namespace Elia.Network
{
    internal class MessageReader : IDisposable
    {
        public TcpClient TcpClient { get; private set; }

        private readonly RequestHandler _handler;

        public MessageReader(TcpClient tcpClient, RequestHandler handler)
        {
            TcpClient = tcpClient;
            _handler = handler;
            BeginReadPackageLenght();
        }

        public bool IsConnected => TcpClient.Connected;

        private NetworkStream Stream => TcpClient.GetStream();

        private byte[] buffer = null!;

        private void BeginReadPackageLenght()
        {
            buffer = new byte[4];

            Stream.BeginRead(buffer, 0, 4, new AsyncCallback(EndReadPackageLenght), null);
        }

        private void EndReadPackageLenght(IAsyncResult result)
        {
            Stream.EndRead(result);

            int packageLenght = BitConverter.ToInt32(buffer, 0);

            buffer = new byte[packageLenght];

            BeginReadMessage(packageLenght);
        }

        private void BeginReadMessage(int packageLenght) => Stream.BeginRead(buffer, 0, packageLenght, new AsyncCallback(EndReadMessage), null);

        private async void EndReadMessage(IAsyncResult result)
        {
            Stream.EndRead(result);

            await _handler.Handle(buffer, TcpClient);

            BeginReadPackageLenght();
        }

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                TcpClient.Dispose();
                _disposed = true;
            }
        }
    }
}
