using Elia.Core.Handler;
using System.Net.Sockets;

namespace Elia.Network.ReadingLoop
{
    public class GenericLoop : ReadingLoopProvider
    {
        private readonly RequestHandler _handler;

        public GenericLoop(RequestHandler handler)
        {
            _handler = handler;
        }

        public override async Task CreateLoopAsync(TcpClient client)
        {
            while (true)
            {
                int contentLenght = await GetContentLenght(client);

                var content = await ReadNetworkStreamAsync(client, contentLenght);

                var response = await _handler.HandleAsync(content);

                await client.GetStream().WriteAsync(response);
            }
        }

        private const int s_intSize = 4;

        private async Task<int> GetContentLenght(TcpClient client)
        {
            byte[] buffer = await ReadNetworkStreamAsync(client, s_intSize);

            return BitConverter.ToInt32(buffer);
        }

        private async Task<byte[]> ReadNetworkStreamAsync(TcpClient client, int contentLenght)
        {
            byte[] buffer = new byte[contentLenght];

            await client.GetStream().ReadAsync(buffer);

            return buffer;
        }
    }
}
