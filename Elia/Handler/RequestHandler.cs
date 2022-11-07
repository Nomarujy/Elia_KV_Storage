using Elia.Handler.Context;
using Elia.Handler.Midlewares;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Elia.Handler
{
    internal class RequestHandler
    {
        private readonly Middleware MiddlewareChain;

        public RequestHandler(Middleware middlewareChain)
        {
            MiddlewareChain = middlewareChain;
        }

        public async Task<byte[]> Handle(byte[] receivedData, TcpClient client)
        {
            Console.WriteLine($"Get request with lenght {receivedData.Length} from {client.GetHashCode()}");

            HandlerContext context = new()
            {
                Request = DeserialzieRequest(receivedData)!,
            };

            if (context.Request is null) context.Response.Status = "Bad request";
            else
            {
                await MiddlewareChain.HandleAsync(context);
            }

            string json = JsonSerializer.Serialize(context.Response);

            return Encoding.UTF8.GetBytes(json);
        }

        private static Request? DeserialzieRequest(byte[] received)
        {
            string json = Encoding.UTF8.GetString(received);

            return JsonSerializer.Deserialize<Request>(json);
        }
    }
}
