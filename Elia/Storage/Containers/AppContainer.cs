using Elia.Storage.Containers.Base;
using EliaLib.Entity;

namespace Elia.Storage.Containers
{
    internal class AppContainer : IContainer
    {
        private readonly Dictionary<string, TopicContainer> _storage = new();
        public void Add(StorageKey key, StorageValue value)
        {
            if (_storage.TryGetValue(key.Topic, out var topic))
            {
                topic.Add(key, value);
            }
            else
            {
                topic = new TopicContainer();
                topic.Add(key, value);

                _storage.Add(key.Topic, topic);
            }
        }

        public StorageValue? Get(StorageKey key)
        {
            if (_storage.TryGetValue(key.Topic, out var topic))
            {
                return topic.Get(key);
            }

            return null;
        }
    }
}
