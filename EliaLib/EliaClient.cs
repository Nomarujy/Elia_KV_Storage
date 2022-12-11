using EliaLib.Entity;
using EliaLib.Network;
using EliaLib.Utilites;
using System.Net.Sockets;

namespace EliaLib
{
    public class EliaClient
    {
        private readonly TcpClient _client;

        public EliaClient(string host, int port)
        {
            _client = new(host, port);
        }

        public async Task<Response> SaveAsync(StorageKey key, StorageValue value)
        {
            var request = CreateRequestMessage(key, value);
            await SendRequest(request);
            return await GetResponse();
        }

        public async Task<Response> GetAsync(StorageKey key)
        {
            var request = CreateRequestMessage(key, null);
            await SendRequest(request);

            return await GetResponse();
        }

        private NetworkMessage CreateRequestMessage(StorageKey key, StorageValue? value)
        {
            Request request = new()
            {
                Key = key,
                Value = value
            };

            return NetworkMessageSerializer.Serialize(request);
        }

        private async Task SendRequest(NetworkMessage message)
        {
            var stream = _client.GetStream();

            await stream.WriteAsync(message.Lenght);
            await stream.WriteAsync(message.Body);
        }

        private async Task<Response> GetResponse()
        {
            var stream = _client.GetStream();

            var response = await NetworkMessageSerializer.DeserializeAsync(stream);

            return ResponseSerializer.Deserialize(response.Body);
        }
    }
}
