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

        public async Task Handle(byte[] receivedData, TcpClient client)
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

            await SendResponse(context.Response, client);
        }

        private static Request? DeserialzieRequest(byte[] received)
        {
            string json = Encoding.UTF8.GetString(received);

            return JsonSerializer.Deserialize<Request>(json);
        }

        private static async Task SendResponse(Response response, TcpClient client)
        {
            string json = JsonSerializer.Serialize(response);

            Console.WriteLine($"Sending to {client.GetHashCode()} {json}");

            byte[] data = Encoding.UTF8.GetBytes(json);

            await client.GetStream().WriteAsync(data);
        }
    }
}
