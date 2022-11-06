using Elia.Handler.Context;

namespace Elia.Handler.Midlewares
{
    internal abstract class Middleware
    {
        protected readonly Middleware? _next;

        protected async Task Next(HandlerContext context)
        {
            if (_next is not null)
            {
                await _next.HandleAsync(context);
            }
        }

        public Middleware(Middleware? next)
        {
            _next = next;
        }

        public abstract Task HandleAsync(HandlerContext context);
    }
}
