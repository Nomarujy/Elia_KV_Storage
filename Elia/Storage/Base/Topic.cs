using EliaLib.Entity;
using System.Collections.Concurrent;

namespace Elia.Storage.Base
{
    internal class Topic
    {
        private readonly ConcurrentDictionary<string, ValueEntity> _values = new();

        public void CreateValue(string key, ValueEntity value)
        {
            _values.AddOrUpdate(key, value, (k, v) => value);
        }

        public ValueEntity? GetValue(string key)
        {
            _values.TryGetValue(key, out ValueEntity? value);

            return value;
        }
    }
}
