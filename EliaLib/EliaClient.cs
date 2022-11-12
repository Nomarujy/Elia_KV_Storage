using EliaLib.Entity;
using EliaLib.Utilites;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace EliaLib
{
    public class EliaClient : IDisposable
    {
        private readonly TcpClient _client;

        public EliaClient(IPEndPoint IP)
        {
            _client = new TcpClient();
            _client.Connect(IP);
        }

        public async Task<Response?> SaveValue(ValuePath path, ValueEntity value)
        {
            var request = new Request()
            {
                ValuePath = path,
                ValueEntity = value,
            };
            
            await SendRequest(request);

            return await ReadResponse();
        }

        public async Task<Response?> ReadValue(ValuePath path)
        {
            var request = new Request()
            {
                ValuePath = path,
            };

            await SendRequest(request);

            return await ReadResponse();
        }

        private void ThrowIfDisconected()
        {
            if (!_client.Connected)
            {
                throw new ObjectDisposedException(nameof(_client));
            } 
        }

        private async Task SendRequest(Request request)
        {
            var data = RequestSerializer.Serialzie(request);

            int lenght = data.Length;

            var byteLenght = BitConverter.GetBytes(lenght);

            ThrowIfDisconected();

            await _client.GetStream().WriteAsync(byteLenght);
            await _client.GetStream().WriteAsync(data);
        }

        private async Task<Response?> ReadResponse()
        {
            ThrowIfDisconected();

            byte[] header = await ReadBytes(4);

            int contentLenght = BitConverter.ToInt32(header);

            byte[] body = await ReadBytes(contentLenght);

            return ResponseSerializer.Deserialize(body);
        }

        private async Task<byte[]> ReadBytes(int count)
        {
            byte[] buffer = new byte[count];

            await _client.GetStream().ReadAsync(buffer);

            return buffer;
        }

        private bool _disposed;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
     
            if (!_disposed)
            {
                _disposed = true;
            }
        }
    }
}
