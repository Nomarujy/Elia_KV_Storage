using Elia.Midlewares.Base;
using Elia.NetCore;
using EliaLib.Network;
using EliaLib.Utilites;
using System.Net.Sockets;

namespace Elia
{
    internal class ServerCore
    {
        private readonly NetworkCore _networkCore;
        private readonly Midleware _midlewareHandle;

        public ServerCore(Midleware requestHandle)
        {
            _networkCore = new NetworkCore();
            _midlewareHandle = requestHandle;
        }

        public Task Start() => _networkCore.StartAsync(ClientHandle);

        private async Task ClientHandle(TcpClient client)
        {
            var stream = client.GetStream();

            while (client.Connected)
            {
                var message = await NetworkMessageSerializer.DeserializeAsync(stream);
                var request = RequestSerializer.Deserialize(message.Body);

                MidlewareContext context = new()
                {
                    Request = request,
                    Response = new(),
                };
                await _midlewareHandle.HandleAsync(context);

                message = NetworkMessageSerializer.Serialize(context.Response);
                await stream.WriteAsync(message.Lenght);
                await stream.WriteAsync(message.Body);
            }
        }
    }
}
