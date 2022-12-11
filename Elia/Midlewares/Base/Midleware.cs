namespace Elia.Midlewares.Base
{
    internal abstract class Midleware
    {
        protected readonly Midleware? _next;

        public Midleware(Midleware? next)
        {
            _next = next;
        }

        protected Task Next(MidlewareContext context)
        {
            if (_next is not null)
            {
                return _next.HandleAsync(context);
            }

            return Task.CompletedTask;
        }

        public abstract Task HandleAsync(MidlewareContext context);
    }
}
