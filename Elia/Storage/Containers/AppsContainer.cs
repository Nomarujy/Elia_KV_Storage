using Elia.Storage.Containers.Base;
using EliaLib.Entity;

namespace Elia.Storage.Containers
{
    internal class AppsContainer : IContainer
    {
        private readonly Dictionary<string, AppContainer> _storage = new();
        public void Add(StorageKey key, StorageValue value)
        {
            if (_storage.TryGetValue(key.App, out var topic))
            {
                topic.Add(key, value);
            }
            else
            {
                topic = new AppContainer();
                topic.Add(key, value);

                _storage.Add(key.App, topic);
            }
        }

        public StorageValue? Get(StorageKey key)
        {
            if (_storage.TryGetValue(key.App, out var topic))
            {
                return topic.Get(key);
            }

            return null;
        }
    }
}
