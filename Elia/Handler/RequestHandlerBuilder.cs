using Elia.Core.Handler;
using Elia.Handler.Midlewares;

namespace Elia.Handler
{
    internal class EliaRequestHandlerProvider
    {
        private readonly List<Type> _midlewares;

        public EliaRequestHandlerProvider()
        {
            _midlewares = new List<Type>()
            {
            };
        }

        public void AddMidleware<T>() where T : Middleware
        {
            _midlewares.Add(typeof(T));
        }

        public RequestHandler Create()
        {
            if (_midlewares.Count == 0) throw new InvalidOperationException("No one midlewares defined");
            int lastIndex = _midlewares.Count - 1;

            Middleware middlewareChain = CreateMidleware(lastIndex, null);

            for (int i = lastIndex - 1; i >= 0; i++)
            {
                middlewareChain = CreateMidleware(i, middlewareChain);
            }

            return new EliaRequestHandler(middlewareChain);
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
