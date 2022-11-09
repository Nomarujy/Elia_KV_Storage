using Elia.Core.Handler;
using Elia.Handler.Context;
using Elia.Handler.Midlewares;
using System.Text;
using System.Text.Json;

namespace Elia.Handler
{
    internal class EliaRequestHandler : RequestHandler
    {
        private readonly Middleware MiddlewareChain;

        public EliaRequestHandler(Middleware middlewareChain)
        {
            MiddlewareChain = middlewareChain;
        }

        public override async Task<byte[]> HandleAsync(byte[] receivedData)
        {
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
