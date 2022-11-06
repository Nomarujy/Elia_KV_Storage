using Elia.Handler.Midlewares;

namespace Elia.Handler
{
    internal class RequestHandlerBuilder
    {
        private readonly List<Type> _midlewares;

        public RequestHandlerBuilder()
        {
            _midlewares = new List<Type>()
            {
            };
        }

        public void AddMidleware<T>() where T : Middleware
        {
            _midlewares.Add(typeof(T));
        }

        public RequestHandler Build()
        {
            int lastIndex = _midlewares.Count - 1;

            Middleware middlewareChain = CreateMidleware(lastIndex, null);

            for (int i = lastIndex - 1; i >= 0; i++)
            {
                middlewareChain = CreateMidleware(i, middlewareChain);
            }

            return new RequestHandler(middlewareChain);
        }

        private Middleware CreateMidleware(int index, Middleware? next)
        {
            var midleware = Activator.CreateInstance(_midlewares[index], next);

            if (midleware is null || midleware is not Middleware)
            {
                throw new ArgumentException("Bad midlewares");
            }

            return (Middleware)midleware;
        }
    }
}
