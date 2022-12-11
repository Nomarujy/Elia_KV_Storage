using Elia.Storage.Containers.Base;
using EliaLib.Entity;

namespace Elia.Storage.Containers
{
    internal class TopicContainer : IContainer
    {
        private readonly Dictionary<string, StorageValue> _storage = new();
        public void Add(StorageKey key, StorageValue value)
        {
            if (_storage.ContainsKey(key.Name))
            {
                _storage[key.Name] = value;
            }
            else
            {
                _storage.Add(key.Name, value);
            }
        }

        public StorageValue? Get(StorageKey key)
        {
            _storage.TryGetValue(key.Name, out var value);
            return value;
        }
    }
}
