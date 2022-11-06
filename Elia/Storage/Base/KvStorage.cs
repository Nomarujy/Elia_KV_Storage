using System.Collections.Concurrent;

namespace Elia.Storage.Base
{
    internal abstract class KvStorage<T> where T : class, new()
    {
        private readonly ConcurrentDictionary<string, T> _value = new();

        public T? Get(string key)
        {
            if (_value.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        public T GetOrCreate(string key)
        {
            return _value.GetOrAdd(key, new T());
        }
    }
}
