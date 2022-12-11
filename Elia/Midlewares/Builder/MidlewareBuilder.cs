using Elia.Midlewares.Base;

namespace Elia.Midlewares.Builder
{
    internal class MidlewareBuilder
    {
        private readonly List<Type> midlewares = new();

        public void Add<T>() where T : Midleware
        {
            midlewares.Add(typeof(T));
        }

        public Midleware Build()
        {
            Midleware next = CreateInstance(0, null);

            for (int i = 1; i < midlewares.Count; i++)
            {
                next = CreateInstance(i, next);
            }
            return next;
        }

        private Midleware CreateInstance(int index, Midleware? next)
        {
            return (Midleware)Activator.CreateInstance(midlewares[index], next)!;
        }
    }
}
